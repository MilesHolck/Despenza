using DespenzaLib.Models;
using DespenzaLib.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages.RecipeSites
{
    public class DoneRecipeModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;

        public List<Recipe> DoneRecipes { get; set; } = new();

        public DoneRecipeModel(IRepository<Recipe> recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        public async Task OnGetAsync()
        {
            // Her henter vi KUN de opskrifter, der er markeret som gemte kopier
            DoneRecipes = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                    .ThenInclude(l => l.Ware)
                .Where(r => r.IsSavedCopy == true) // KUN færdige opskrifter her!
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            
            await _recipeRepo.DeleteAsync(id);

            
            return RedirectToPage();
        }
    }
}
