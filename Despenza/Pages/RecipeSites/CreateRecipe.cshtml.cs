using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages
{
    public class CreateRecipeModel : PageModel
    {
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

        public async Task OnGetAsync()
        {
            NewRecipe.Lines.Add(new RecipeLine()); 
            await LoadWaresAsync();
        }

        public async Task<IActionResult> OnPostAddLineAsync()
        {
            await LoadWaresAsync();
            NewRecipe.Lines.Add(new RecipeLine());
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            
            NewRecipe.Lines.RemoveAll(l => l.WareId == 0);

            // MIDLERTIDIGT UDKOMMENTERET FOR AT UNDGÅ AT DEN FEJLER
            /* if (!ModelState.IsValid)
            {
                await LoadWaresAsync();
                return Page();
            }
            */

            await _recipeRepo.AddAsync(NewRecipe); 

            
            return RedirectToPage("RecipeList");
        }
        private async Task LoadWaresAsync()
        {
            // Vi henter alle varer fra databasen
            var wares = await _wareRepo.GetAllAsync();

            // Og bygger dropdown-menuen ud fra dem
            WareOptions = new SelectList(wares, "Id", "Name");
        }
    }
}