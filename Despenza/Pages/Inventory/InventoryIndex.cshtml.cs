using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Despenza.Pages.Inventory
{
    [Authorize (Roles = "Admin,User")] 
    public class InventoryIndexModel : PageModel
    {

        private readonly IInventoryService _inventoryService;

        public List<Ingredient> Ingredients { get; set; } = new();    
        public List<SemiProduct> SemiProducts { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<InventoryItem> InventoryItems { get; set; } = new();

        public InventoryIndexModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        public async Task OnGetAsync()
        {
            Ingredients = await _inventoryService.GetAllIngredientsAsync();
            SemiProducts = await _inventoryService.GetAllSemiProductsAsync();
            Products = await _inventoryService.GetAllProductsAsync();
            InventoryItems = await _inventoryService.GetInventoryItemsWithWareAsync();
        }
    }
}
