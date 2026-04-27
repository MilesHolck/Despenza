using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CreateIngredientModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        [BindProperty]
        public InventoryItem InventoryItem { get; set; } = new();

        public CreateIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _inventoryService.CreateInventoryItemsAsync(InventoryItem);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}