using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Inventory
{
    [Authorize]
    public class WasteOverviewModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public List<WasteRegistration> WasteRegistrations { get; set; } = new();

        public WasteOverviewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task OnGetAsync()
        {
            WasteRegistrations = await _inventoryService.GetAllWasteRegistrationsAsync();
        }
    }
    
}
