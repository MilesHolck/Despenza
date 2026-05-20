using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Despenza.Pages.Inventory
{
    [Authorize(Roles = "Admin")]
    public class RegisterWasteModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public RegisterWasteModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public string WareType { get; set; } = "Ingredient";

        [BindProperty]
        public int WareId { get; set; }

        [BindProperty]
        public string Reason { get; set; } = string.Empty;

        [BindProperty]
        public string? CustomReason { get; set; } = string.Empty;

        [BindProperty]
        public decimal Quantity { get; set; }

        public List<SelectListItem> WareTypeOptions { get; set; } = new();
        public List<SelectListItem> WareOptions { get; set; } = new();
        public List<SelectListItem> ReasonOptions { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            await LoadDropdownsAsync();

            if (action != "registerWaste")
            {
                ModelState.Clear();
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Quantity <= 0)
            {
                ModelState.AddModelError("", "Dude... Du kan ikke registrere 0 eller minus.");
                return Page();
            }

            string finalReason = Reason;

            if (Reason == "Andet")
            {
                if (string.IsNullOrWhiteSpace(CustomReason))
                {
                    ModelState.AddModelError("CustomReason", "Du skal skrive en kommentar, når du vælger 'Andet'.");
                    return Page();
                }

                finalReason = $"{CustomReason}";
            }

            try
            {
               
                await _inventoryService.RegisterWasteAsync(WareId, WareType, Quantity, Reason, finalReason);
            }
            catch (Exception ex)
            {
               
                string trueErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                ModelState.AddModelError("", $"Fejl ved gem: {trueErrorMessage}");
                return Page();
            }

            return RedirectToPage("/Inventory/WasteOverview");
        }

        private async Task LoadDropdownsAsync()
        {
            WareTypeOptions = new List<SelectListItem>
    {
        new SelectListItem { Value = "Ingredient", Text = "Ingrediens" },
        new SelectListItem { Value = "SemiProduct", Text = "Halvfabrikat" },
        new SelectListItem { Value = "Helfabrikat", Text = "Helfabrikat" }
    };

            ReasonOptions = new List<SelectListItem>
    {
        new SelectListItem { Value = "Tabt", Text = "Tabt på gulvet" },
        new SelectListItem { Value = "Udløbet", Text = "Gået over dato" },
        new SelectListItem { Value = "Brændt", Text = "Brændt på" },
        new SelectListItem { Value = "Fejlproduktion", Text = "Fejlproduktion" },
        new SelectListItem { Value = "Andet", Text = "Andet" }
    };

            
            var inventoryItems = await _inventoryService.GetInventoryItemsWithWareAsync();

            if (WareType == "Ingredient")
            {
                WareOptions = inventoryItems
                    .Where(i => i.Ware is Ingredient)
                    .Select(i => new SelectListItem
                    {
                        Value = i.WareId.ToString(),
                        Text = i.Ware.Name
                    }).ToList();
            }
            else if (WareType == "SemiProduct")
            {
                WareOptions = inventoryItems
                    .Where(i => i.Ware is SemiProduct)
                    .Select(i => new SelectListItem
                    {
                        Value = i.WareId.ToString(),
                        Text = i.Ware.Name
                    }).ToList();
            }
            else if (WareType == "Product")
            {
                WareOptions = inventoryItems
                    .Where(i => i.Ware is Product)
                    .Select(i => new SelectListItem
                    {
                        Value = i.WareId.ToString(),
                        Text = i.Ware.Name
                    }).ToList();
            }
        }
    }
}
