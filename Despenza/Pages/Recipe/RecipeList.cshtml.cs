using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DespenzaLib.Models;
using DespenzaLib.Data; 

namespace Despenza.Pages
{
    public class RecipeListModel : PageModel
    {
        private readonly AppDbContext _context;

        
        public RecipeListModel(AppDbContext context)
        {
            _context = context;
        }


        public List<DespenzaLib.Models.Recipe> Recipes { get; set; } = new();

        public async Task OnGetAsync()
        {
            
            Recipes = await _context.Recipes
                .Include(r => r.Lines)
                    .ThenInclude(l => l.Ware)
                .ToListAsync();
        }
    }
}