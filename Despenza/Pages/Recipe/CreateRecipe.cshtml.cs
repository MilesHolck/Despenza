using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DespenzaLib.Models;
using DespenzaLib.Data;

namespace Despenza.Pages
{
    public class CreateRecipeModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateRecipeModel(AppDbContext context) => _context = context;

        [BindProperty]
        public DespenzaLib.Models.Recipe NewRecipe { get; set; } = new();

        public SelectList WareOptions { get; set; }

        public async Task OnGetAsync()
        {
            NewRecipe.Lines.Add(new RecipeLine()); // Start med én linje
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
            // Fjern de linjer hvor der ikke er valgt en vare (WareId == 0)
            NewRecipe.Lines.RemoveAll(l => l.WareId == 0);

            // MIDLERTIDIGT UDKOMMENTERET FOR AT UNDGÅ AT DEN FEJLER I STILHED
            /* if (!ModelState.IsValid)
            {
                await LoadWaresAsync();
                return Page();
            }
            */

            // Tilføj opskriften til databasen og gem
            _context.Recipes.Add(NewRecipe);
            await _context.SaveChangesAsync();

            // Hop direkte over til din liste-side, når den er gemt
            return RedirectToPage("RecipeList");
        }

        private async Task LoadWaresAsync()
        {
            var wares = await _context.Wares.ToListAsync();
            WareOptions = new SelectList(wares, "Id", "Name");
        }
    }
}