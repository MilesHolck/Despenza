using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
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


        // Vi ændrer 'decimal scale' til 'string scale'
        public async Task<IActionResult> OnGetAsync(int? scaleRecipeId, string scale = "1")
        {
            // Hent opskrifterne
            Recipes = await _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .ThenInclude(l => l.Ware)
                .Where(r => r.IsSavedCopy == false)
                .ToListAsync();

            // Vi sørger for at alt læses rigtigt uanset komma eller punktum
            string normalizedScale = scale.Replace(",", ".");

            // Forsøg at omdanne det til en præcis decimal
            if (scaleRecipeId.HasValue && decimal.TryParse(normalizedScale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal parsedScale))
            {
                // Hvis tallet ikke er 1, så skal vi i gang med matematikken
                if (parsedScale != 1.0m)
                {
                    var recipeToScale = Recipes.FirstOrDefault(r => r.Id == scaleRecipeId.Value);

                    if (recipeToScale != null)
                    {
                        recipeToScale.QuantityOfProduct = recipeToScale.QuantityOfProduct * parsedScale;
                        recipeToScale.RecipeScale = parsedScale;

                        foreach (var line in recipeToScale.Lines)
                        {
                            line.Quantity = line.Quantity * parsedScale;
                        }

                        // VIGTIGT: Dette gemmer ID'et, så menuen holdes åben!
                        ViewData["ActiveRecipeId"] = scaleRecipeId.Value;
                    }
                }
            }

            return Page();
        }


        // 1. Vi ændrer parameteren til 'string scale'
        public async Task<IActionResult> OnPostSaveCopyAsync(int id, string scale = "1", List<int> checkedLines = null)
        {
            // 2. Vi laver vores lille oversætter-trick igen
            string normalizedScale = scale.Replace(",", ".");
            decimal parsedScale = 1.0m;
            decimal.TryParse(normalizedScale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedScale);

            // Henter originalen
            var originalRecipe = await _recipeRepo.GetQueryable()
                .AsNoTracking()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (originalRecipe == null) return NotFound();

            // Bygger din dokumentations-kopi (nu med parsedScale!)
            var newSavedRecipe = new Recipe
            {
                Name = originalRecipe.Name,
                Description = originalRecipe.Description,
                IsSavedCopy = true,
                DateSaved = DateTime.Now,
                RecipeScale = parsedScale,
                QuantityOfProduct = originalRecipe.QuantityOfProduct * parsedScale
            };

            if (originalRecipe.Lines != null && originalRecipe.Lines.Any())
            {
                newSavedRecipe.Lines = new List<RecipeLine>();

                foreach (var line in originalRecipe.Lines)
                {
                    newSavedRecipe.Lines.Add(new RecipeLine
                    {
                        Quantity = line.Quantity * parsedScale,
                        WareId = line.WareId,
                        // Tjekker om den blev krydset af!
                        IsChecked = checkedLines != null && checkedLines.Contains(line.Id)
                    });
                }
            }

            await _recipeRepo.AddAsync(newSavedRecipe);

            // VIGTIGT: Tjek at denne filsti passer med hvad din "DoneRecipe" fil hedder!
            return RedirectToPage("DoneRecipe");
        }

        

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            
            await _recipeRepo.DeleteAsync(id);

            
            return RedirectToPage();
        }

    }
}