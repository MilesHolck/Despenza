using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Despenza.Pages
{
    public class CreateRecipeModel : PageModel

    {
        public List<Recipe> Recipes { get; set; }
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly IRepository<Wares> _wareRepo;
        private readonly IRepository<SemiProduct> _semiProductRepo;
        private readonly IRepository<Product> _productRepo;

        public CreateRecipeModel(IRepository<Recipe> recipeRepo, IRepository<Wares> wareRepo, IRepository<SemiProduct> semiProductRepo, IRepository<Product> productRepo)
        {
            _recipeRepo = recipeRepo;
            _wareRepo = wareRepo;
            _semiProductRepo = semiProductRepo;
            _productRepo = productRepo;

        }

        [BindProperty]
        public Recipe NewRecipe { get; set; } = new();

        [BindProperty]
        public List<Allergen> SelectedAllergens { get; set; } = new();
        
        public SelectList WareOptions { get; set; }


        public async Task<IActionResult> OnGetAsync(int? scaleRecipeId, decimal scale = 1.0m)
        {

            //gemmer kopi 
            Recipes = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .ThenInclude(l => l.Ware)
                .Where(r => r.IsSavedCopy == false)
                .ToListAsync();

            //skalering 
            if (scaleRecipeId.HasValue && scale != 1.0m)
            {
                var recipeToScale = Recipes.FirstOrDefault(r => r.Id == scaleRecipeId.Value);

                if (recipeToScale != null)
                {

                    recipeToScale.QuantityOfProduct = recipeToScale.QuantityOfProduct * scale;
                    recipeToScale.RecipeScale = scale;

                    foreach (var line in recipeToScale.Lines)
                    {
                        line.Quantity = line.Quantity * scale;
                    }

                    ViewData["ActiveRecipeId"] = scaleRecipeId.Value;
                }
            }

            return Page();
        }

        //tilføj ny ingrediens linje 
        public async Task<IActionResult> OnPostAddLineAsync()
        {
            await LoadWaresAsync();
            NewRecipe.Lines.Add(new RecipeLine());
            return Page();
        }

        //lægger ingrediens mængden sammen 
        public async Task<IActionResult> OnPostCalculateTotalAsync()
        {
            
            ModelState.Clear();

           
            var wares = await _wareRepo.GetQueryable().ToListAsync();
            WareOptions = new SelectList(wares, "Id", "Name");

           
            if (NewRecipe.Lines != null && NewRecipe.Lines.Count > 0)
            {
                decimal totalWeight = NewRecipe.Lines.Sum(l => l.Quantity);
                NewRecipe.QuantityOfProduct = totalWeight;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!User?.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Index");
            }

            NewRecipe.Lines.RemoveAll(l => l.WareId == 0);
            NewRecipe.RecipeScale = 1.0m;
            NewRecipe.IsSavedCopy = false;
            NewRecipe.DateSaved = DateTime.Now;

            // allergener 
            if (NewRecipe.Lines != null && NewRecipe.Lines.Count > 0)
            {
                NewRecipe.QuantityOfProduct = NewRecipe.Lines.Sum(l => l.Quantity);
                foreach (var allergen in SelectedAllergens)
                {
                    NewRecipe.RecipeAllergens.Add(new RecipeAllergen { Allergen = allergen });
                }

                var userIdString = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                int.TryParse(userIdString, out int currentUserId);
                NewRecipe.UserId = currentUserId; 

               
                await _recipeRepo.AddAsync(NewRecipe);

                

                return RedirectToPage("RecipeList");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostScaleSaveAsync()
        {
            NewRecipe.Lines.RemoveAll(l => l.WareId == 0);
            NewRecipe.RecipeScale = 1.0m;
            NewRecipe.IsSavedCopy = false;
            NewRecipe.DateSaved = DateTime.Now;

            await _recipeRepo.AddAsync(NewRecipe);

            return RedirectToPage("RecipeList");
        }

        private async Task LoadWaresAsync()
        {

            var wares = await _wareRepo.GetAllAsync();

            WareOptions = new SelectList(wares, "Id", "Name");
        }
    }
}