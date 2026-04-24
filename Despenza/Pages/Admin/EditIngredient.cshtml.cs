using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    public class EditIngredientModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        [BindProperty]
        public Ingredients Ingredient { get; set; } = new();

        public EditIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ingredient = await _inventoryService.GetIngredientByIdAsync(id);

            if (ingredient == null)
                return NotFound();

            Ingredient = ingredient;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _inventoryService.UpdateIngredientAsync(Ingredient);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}
