using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Despenza.Pages
{
    public class RecipeListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchText { get; set; }
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly IInventoryService _inventoryService;

        public List<Recipe> Recipes { get; set; } = new();
        public Dictionary<int, decimal> LineStockAmount { get; set; } = new();

        public RecipeListModel(IRepository<Recipe> recipeRepo, IInventoryService inventoryService)
        {
            _recipeRepo = recipeRepo;
            _inventoryService = inventoryService;
        }


        public async Task<IActionResult> OnGetAsync(int? scaleRecipeId, string scale = "1")
        {
            var query = _recipeRepo.GetQueryable()
                .Include(r => r.Lines)
                .ThenInclude(l => l.Ware)
                .Where(r => r.IsSavedCopy == false);           

            SearchText = SearchText?.Trim();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(r => r.Name.Contains(SearchText));
            }

            Recipes = await query.ToListAsync();
            

            string normalizedScale = scale.Replace(",", ".");


            if (scaleRecipeId.HasValue && decimal.TryParse(normalizedScale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal parsedScale))
            {

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


                        ViewData["ActiveRecipeId"] = scaleRecipeId.Value;
                    }
                }
            }

            var inventory = await _inventoryService.GetAllInventoryItemsAsync();

            foreach (var recipe in Recipes)
            {
                if (recipe.Lines != null)
                {
                    foreach (var line in recipe.Lines)
                    {
                        var stockQuantity = inventory
                            .Where(i => i.WareId == line.WareId)
                            .Sum(i => i.QuantityInStock);

                        LineStockAmount[line.Id] = stockQuantity;
                    }
                }
            }


            return Page();
        }

        public async Task<IActionResult> OnPostSaveDraftAsync(int id, string scale = "1", List<int> checkedLines = null)
        {
            string normalizedScale = scale.Replace(",", ".");
            decimal parsedScale = 1.0m;
            decimal.TryParse(normalizedScale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedScale);

            var originalRecipe = await _recipeRepo.GetQueryable()
                .AsNoTracking()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (originalRecipe == null) return NotFound();

            var newDraftRecipe = new Recipe
            {
                Name = originalRecipe.Name,
                Description = originalRecipe.Description,
                IsSavedCopy = false,
                IsDraft = true,
                DateSaved = DateTime.Now,
                RecipeScale = parsedScale,
                QuantityOfProduct = originalRecipe.QuantityOfProduct * parsedScale
            };

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int userId))
            {
                newDraftRecipe.UserId = userId;
            }

            if (originalRecipe.Lines != null && originalRecipe.Lines.Any())
            {
                newDraftRecipe.Lines = new List<RecipeLine>();
                foreach (var line in originalRecipe.Lines)
                {
                    newDraftRecipe.Lines.Add(new RecipeLine
                    {
                        Quantity = line.Quantity * parsedScale,
                        WareId = line.WareId,
                        IsChecked = checkedLines != null && checkedLines.Contains(line.Id)
                    });
                }
            }

          

            await _recipeRepo.AddAsync(newDraftRecipe);


            return RedirectToPage("/RecipeSites/RecipeDraft");
        }


        public async Task<IActionResult> OnPostSaveCopyAsync(int id, string scale = "1", List<int> checkedLines = null)
        {
            string normalizedScale = scale.Replace(",", ".");
            decimal parsedScale = 1.0m;
            decimal.TryParse(normalizedScale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedScale);

            var originalRecipe = await _recipeRepo.GetQueryable()
                .AsNoTracking()
                .Include(r => r.Lines)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (originalRecipe == null) return NotFound();

            var newSavedRecipe = new Recipe
            {
                Name = originalRecipe.Name,
                Description = originalRecipe.Description,
                IsSavedCopy = true,
                DateSaved = DateTime.Now,
                RecipeScale = parsedScale,
                QuantityOfProduct = originalRecipe.QuantityOfProduct * parsedScale
            };

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int userId))
            {
                newSavedRecipe.UserId = userId;
            }

            if (originalRecipe.Lines != null && originalRecipe.Lines.Any())
            {
                newSavedRecipe.Lines = new List<RecipeLine>();

                foreach (var line in originalRecipe.Lines)
                {
                    newSavedRecipe.Lines.Add(new RecipeLine
                    {
                        Quantity = line.Quantity * parsedScale,
                        WareId = line.WareId,
                        IsChecked = checkedLines != null && checkedLines.Contains(line.Id)
                    });
                }
            }

            await _recipeRepo.AddAsync(newSavedRecipe);

            return RedirectToPage("DoneRecipe");
        }



        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {

            await _recipeRepo.DeleteAsync(id);


            return RedirectToPage();
        }

    }
}