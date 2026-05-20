using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Despenza.Pages.RecipeSites
{

    [Authorize]
    public class RecipeDraftModel : PageModel
    {
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly IRepository<InventoryItem> _inventoryRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<SemiProduct> _semiProductRepo;

        public RecipeDraftModel(
            IRepository<Recipe> recipeRepo,
            IRepository<InventoryItem> inventoryRepo,
            IRepository<Product> productRepo,
            IRepository<SemiProduct> semiProductRepo)
        {
            _recipeRepo = recipeRepo;
            _inventoryRepo = inventoryRepo;
            _productRepo = productRepo;
            _semiProductRepo = semiProductRepo;
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

            
            int currentUserId = 0;
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int parsedUserId))
            {
                currentUserId = parsedUserId;
            }

            
            draftRecipe.IsDraft = false;
            draftRecipe.IsSavedCopy = true;
            draftRecipe.DateSaved = DateTime.Now;

           
            if (draftRecipe.Lines != null && draftRecipe.Lines.Any())
            {
                foreach (var line in draftRecipe.Lines)
                {
                    bool isLineChecked = checkedLines != null && checkedLines.Contains(line.Id);
                    line.IsChecked = isLineChecked;

                    
                    if (isLineChecked)
                    {
                        var inventoryItem = await _inventoryRepo.GetQueryable()
                            .FirstOrDefaultAsync(i => i.WareId == line.WareId);

                        if (inventoryItem != null)
                        {
                           
                            inventoryItem.QuantityInStock -= line.Quantity;
                            await _inventoryRepo.UpdateAsync(inventoryItem);
                        }
                    }
                }
            }

            
            if (draftRecipe.IsProduct)
            {
                var product = await _productRepo.GetQueryable()
                    .FirstOrDefaultAsync(p => p.Name == draftRecipe.Name);

                if (product == null)
                {
                    product = new Product
                    {
                        Name = draftRecipe.Name,
                        RecipeId = draftRecipe.Id,
                        UserId = currentUserId
                    };
                    await _productRepo.AddAsync(product);
                }

                decimal producedAmount = draftRecipe.QuantityOfProduct;
                var inventoryItem = await _inventoryRepo.GetQueryable()
                    .FirstOrDefaultAsync(i => i.WareId == product.Id);

                if (inventoryItem != null)
                {
                    inventoryItem.QuantityInStock += producedAmount;
                    await _inventoryRepo.UpdateAsync(inventoryItem);
                }
                else
                {
                    await _inventoryRepo.AddAsync(new InventoryItem
                    {
                        WareId = product.Id,
                        QuantityInStock = producedAmount
                    });
                }
            }

            
            if (draftRecipe.IsSemiProduct)
            {
                var semiProduct = await _semiProductRepo.GetQueryable()
                    .FirstOrDefaultAsync(sp => sp.Name == draftRecipe.Name);

                if (semiProduct == null)
                {
                    semiProduct = new SemiProduct
                    {
                        Name = draftRecipe.Name,
                        RecipeId = draftRecipe.Id,
                        UserId = currentUserId
                    };
                    await _semiProductRepo.AddAsync(semiProduct);
                }

                decimal producedAmountInGrams = draftRecipe.QuantityOfProduct;
                var inventoryItem = await _inventoryRepo.GetQueryable()
                    .FirstOrDefaultAsync(i => i.WareId == semiProduct.Id);

                if (inventoryItem != null)
                {
                    inventoryItem.QuantityInStock += producedAmountInGrams;
                    await _inventoryRepo.UpdateAsync(inventoryItem);
                }
                else
                {
                    await _inventoryRepo.AddAsync(new InventoryItem
                    {
                        WareId = semiProduct.Id,
                        QuantityInStock = producedAmountInGrams
                    });
                }
            }

            
            await _recipeRepo.UpdateAsync(draftRecipe);

            return RedirectToPage("/RecipeSites/DoneRecipe");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _recipeRepo.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}