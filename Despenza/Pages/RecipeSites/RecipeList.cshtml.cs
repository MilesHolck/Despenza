using DespenzaLib.Data; 
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages
{
    public class RecipeListModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;

        public List<Recipe> Recipes { get; set; } = new(); 

        public RecipeListModel(IRepository<Recipe> recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }


        public async Task OnGetAsync()
        {
            // 1. Vi henter opskriften synkront (lynhurtigt)
            IQueryable<Recipe> query = _recipeRepo.GetQueryable();

            // 2. Vi bygger videre på den og kalder databasen asynkront
            Recipes = await query
                .Include(r => r.Lines)
                    .ThenInclude(l => l.Ware)
                .ToListAsync();
        }
      
    }
}