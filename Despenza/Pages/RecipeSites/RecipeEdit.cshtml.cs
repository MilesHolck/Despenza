using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DespenzaLib.Models;
using DespenzaLib.Repos; 

namespace Despenza.Pages.RecipeSites
{
    public class RecipeEditModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly IRepository<Wares> _wareRepo; 

        public RecipeEditModel(IRepository<Recipe> recipeRepo, IRepository<Wares> wareRepo)
        {
            _recipeRepo = recipeRepo;
            _wareRepo = wareRepo;
        }

        [BindProperty]
        public Recipe RecipeToEdit { get; set; }

        
        public SelectList WareOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            RecipeToEdit = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (RecipeToEdit == null) return NotFound();

            await LoadWaresAsync();
            return Page();


        }

        
        private async Task LoadWaresAsync()
        {
            var wares = await _wareRepo.GetQueryable().ToListAsync();
            WareOptions = new SelectList(wares, "Id", "Name");
        }

        
        public async Task<IActionResult> OnPostAddLineAsync()
        {
            if (RecipeToEdit.Lines == null) RecipeToEdit.Lines = new List<RecipeLine>();

            RecipeToEdit.Lines.Add(new RecipeLine());

            await LoadWaresAsync(); 
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
           
            ModelState.Remove("RecipeToEdit.User");
            ModelState.Remove("RecipeToEdit.UserId");

            var lineKeys = ModelState.Keys.Where(k => k.StartsWith("RecipeToEdit.Lines")).ToList();
            foreach (var key in lineKeys)
            {
                ModelState.Remove(key);
            }

            
            if (!ModelState.IsValid)
            {
                await LoadWaresAsync();
                return Page();
            }

           
            var recipeInDb = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == RecipeToEdit.Id);

            if (recipeInDb == null) return NotFound();


            recipeInDb.Name = RecipeToEdit.Name;
            recipeInDb.Description = RecipeToEdit.Description;
            recipeInDb.QuantityOfProduct = RecipeToEdit.QuantityOfProduct;

            
            recipeInDb.Lines.Clear();

            if (RecipeToEdit.Lines != null)
            {
                foreach (var line in RecipeToEdit.Lines)
                {
                    if (line.WareId > 0)
                    {
                        recipeInDb.Lines.Add(new RecipeLine
                        {
                            WareId = line.WareId,
                            Quantity = line.Quantity
                        });
                    }
                }
            }

            
            await _recipeRepo.UpdateAsync(recipeInDb);

            
            if (recipeInDb.IsDraft)
            {
                return RedirectToPage("/RecipeSites/RecipeDraft");
            }
            else if (recipeInDb.IsSavedCopy)
            {
                return RedirectToPage("/RecipeSites/DoneRecipe");
            }
            else
            {
                return RedirectToPage("/RecipeSites/RecipeList");
            }
        }
    }
}