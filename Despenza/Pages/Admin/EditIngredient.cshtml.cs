using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditIngredientModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = new();

        public EditIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var inventoryItem = await _inventoryService.GetInventoryItemsByIdAsync(id);

            if (inventoryItem == null)
                return NotFound();

            this.InventoryItem = inventoryItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _inventoryService.UpdateInventoryItemsAsync(InventoryItem);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}
