using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Inventory
{
    public class CreateInventoryItemModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = new();

        [BindProperty]
        public Ingredient Ingredient { get; set; } = new();

        public List<Ingredient> IngredientsList { get; set; }

        [BindProperty]
        public decimal QuantityInStock { get; set; }

        public CreateInventoryItemModel (IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        //public void OnGet()
        //{
        //}
        public async Task OnGetAsync()
        {
            IngredientsList = await _inventoryService.GetAllIngredientsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //    return Page();

            //var inventoryItem = new InventoryItem
            //{
            //    Ware = Ingredient,
            //    QuantityInStock = QuantityInStock
            //};

            await _inventoryService.CreateInventoryItemsAsync(InventoryItem);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}
