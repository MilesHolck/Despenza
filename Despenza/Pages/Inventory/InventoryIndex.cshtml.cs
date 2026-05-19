using DespenzaLib.Models;
using DespenzaLib.Services;
using DespenzaLib.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Despenza.Pages.Inventory
{
    [Authorize(Roles = "Admin,User")]
    public class InventoryIndexModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        private readonly IRepository<SemiProduct> _semiProductRepo;
        private readonly IRepository<Product> _productRepo;

        public List<Ingredient> Ingredients { get; set; } = new();
        public List<SemiProduct> SemiProducts { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<InventoryItem> InventoryItems { get; set; } = new();

        public InventoryIndexModel(
            IInventoryService inventoryService,
            IRepository<SemiProduct> semiProductRepo,
            IRepository<Product> productRepo)
        {
            _inventoryService = inventoryService;
            _semiProductRepo = semiProductRepo;
            _productRepo = productRepo;
        }

        public async Task OnGetAsync()
        {
            Ingredients = await _inventoryService.GetAllIngredientsAsync();
            SemiProducts = await _inventoryService.GetAllSemiProductsAsync();
            Products = await _inventoryService.GetAllProductsAsync();
            InventoryItems = await _inventoryService.GetInventoryItemsWithWareAsync();
        }

        
        public async Task<IActionResult> OnPostDeleteSemiProductAsync(int id)
        {
            await _semiProductRepo.DeleteAsync(id);
            return RedirectToPage();
        }

        
        public async Task<IActionResult> OnPostDeleteProductAsync(int id)
        {
            await _productRepo.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}