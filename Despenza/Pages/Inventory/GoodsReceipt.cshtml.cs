using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Despenza.Pages.Inventory
{
    [Authorize]
    public class GoodsReceiptModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public GoodsReceiptModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public int IngredientId { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public string UnitType { get; set; } = "gram";

        public List<SelectListItem> IngredientOptions { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDropdownsAsync();

            if (Amount <= 0)
            {
                ModelState.AddModelError("", "Mængden skal være større end 0.");
                return Page();
            }

            decimal amountInGrams = UnitType == "kg"
                ? Amount * 1000
                : Amount;

            await _inventoryService.ReceiveIngredientAsync(IngredientId, amountInGrams);

            return RedirectToPage("/Inventory/InventoryIndex");
        }

        private async Task LoadDropdownsAsync()
        {
            var ingredients = await _inventoryService.GetAllIngredientsAsync();

            IngredientOptions = ingredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Name} - {i.KiloPrice:F2} kr/kg"
            }).ToList();
        }
    }
}