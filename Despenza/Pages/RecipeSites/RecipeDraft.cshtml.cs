using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Despenza.Pages.RecipeSites
{
    public class RecipeDraftModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;

        public RecipeDraftModel(IRepository<Recipe> recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }
        public List<Recipe> Recipes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Recipes = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .ThenInclude(l => l.Ware)
                .Include(r => r.User)
                .Where(r => r.IsDraft == true)
                .ToListAsync();
        }


        public async Task<IActionResult> OnPostCompleteDraftAsync(int id, List<int> checkedLines = null)
        {

            var draftRecipe = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (draftRecipe == null)
            {
                return NotFound();
            }


            draftRecipe.IsDraft = false;
            draftRecipe.IsSavedCopy = true;
            draftRecipe.DateSaved = DateTime.Now;


            if (draftRecipe.Lines != null && draftRecipe.Lines.Any())
            {
                foreach (var line in draftRecipe.Lines)
                {

                    line.IsChecked = checkedLines != null && checkedLines.Contains(line.Id);
                }
            }


            await _recipeRepo.UpdateAsync(draftRecipe);


            return RedirectToPage("/RecipeSites/DoneRecipe");
        }

    }

}

