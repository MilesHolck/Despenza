using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    public class AllIngredientsModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public List<InventoryItem> IngredientsList { get; set; } = new();

        public AllIngredientsModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task OnGetAsync()
        {
            IngredientsList = await _inventoryService.GetAllInventoryItemsAsync();
        }
    }
}