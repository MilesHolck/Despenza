using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Despenza.Pages
{
    public class CreateRecipeModel : PageModel

    {
        public List<Recipe> Recipes { get; set; }
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly IRepository<Wares> _wareRepo;

        public CreateRecipeModel(IRepository<Recipe> recipeRepo, IRepository<Wares> wareRepo)
        {
            _recipeRepo = recipeRepo;
            _wareRepo = wareRepo;
        }

        [BindProperty]
        public Recipe NewRecipe { get; set; } = new();

        public SelectList WareOptions { get; set; }

       
        public async Task<IActionResult> OnGetAsync(int? scaleRecipeId, decimal scale = 1.0m)
        {
            
            Recipes = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .ThenInclude(l => l.Ware)
                .Where(r => r.IsSavedCopy == false)
                .ToListAsync();

            
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

        public async Task<IActionResult> OnPostAddLineAsync()
        {
            await LoadWaresAsync();
            NewRecipe.Lines.Add(new RecipeLine());
            return Page();
        }



        
        public async Task<IActionResult> OnPostSaveAsync()
        {
            // Fjerner tomme ingredienser
            NewRecipe.Lines.RemoveAll(l => l.WareId == 0);

           
            NewRecipe.RecipeScale = 1.0m;

            
            NewRecipe.IsSavedCopy = false;
            NewRecipe.DateSaved = DateTime.Now;

            
            await _recipeRepo.AddAsync(NewRecipe);

            
            return RedirectToPage("RecipeList");
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