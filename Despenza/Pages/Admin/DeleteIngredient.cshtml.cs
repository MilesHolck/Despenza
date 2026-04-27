using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    public class DeleteIngredientModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public InventoryItem? InventoryItem { get; set; }

        public DeleteIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            InventoryItem = await _inventoryService.GetInventoryItemsByIdAsync(id);

            if (InventoryItem == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _inventoryService.DeleteInventoryItemsAsync(id);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}
