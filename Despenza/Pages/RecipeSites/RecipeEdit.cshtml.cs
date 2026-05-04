using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DespenzaLib.Models;
using DespenzaLib.Repos; // Ret til jeres rigtige namespaces

namespace Despenza.Pages.RecipeSites
{
    public class EditRecipeModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;

        public EditRecipeModel(IRepository<Recipe> recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        // BindProperty gør, at HTML-formularen automatisk taler sammen med dette objekt
        [BindProperty]
        public Recipe RecipeToEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Hent den opskrift, der skal redigeres
            RecipeToEdit = await _recipeRepo.GetQueryable()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (RecipeToEdit == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipeInDb = await _recipeRepo.GetQueryable()
                .FirstOrDefaultAsync(r => r.Id == RecipeToEdit.Id);

            if (recipeInDb == null)
            {
                return NotFound();
            }

            
            recipeInDb.Name = RecipeToEdit.Name;
            recipeInDb.Description = RecipeToEdit.Description;
            recipeInDb.QuantityOfProduct = RecipeToEdit.QuantityOfProduct;
           
            await _recipeRepo.UpdateAsync(recipeInDb);

           
            if (recipeInDb.IsSavedCopy)
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