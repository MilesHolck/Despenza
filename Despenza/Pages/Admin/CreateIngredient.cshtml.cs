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
        public Ingredient Ingredient { get; set; } = new();
        //[BindProperty]
        //public decimal QuantityInStock { get; set; }
        //public List<Ingredient> Ingredients { get; set; }      



        public CreateIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        //public async Task OnGetAsync()
        //{
        //    Ingredients = await _inventoryService.GetAllIngredientsAsync();
        //}


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Ingredient.Unit = "gram"; // eller "kg"

            //var inventoryItem = new InventoryItem
            //{
            //    Ware = Ingredient,
            //    QuantityInStock = QuantityInStock,
            //    ExpirationDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6))
            //};

            await _inventoryService.CreateIngredientAsync(Ingredient/*inventoryItem*/);

            return RedirectToPage("/Admin/AllIngredients");
        }
    }
}