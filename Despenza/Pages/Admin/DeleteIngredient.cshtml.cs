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

        public Ingredient Ingredient { get; set; }

        public DeleteIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Ingredient = await _inventoryService.GetIngredientByIdAsync(id);

            if (Ingredient == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _inventoryService.DeleteIngredientAsync(id);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}
