using DespenzaLib;
using DespenzaLib.Models;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    //[Authorize(Roles = "Admin")]
    public class CreateIngredientModel : PageModel
    {
        private readonly IInventoryService _inventoryService;            

        [BindProperty]
        public Ingredient Ingredient { get; set; } = new();     
        public List<Ingredient> Ingredients { get; set; }



        public CreateIngredientModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task OnGetAsync()
        {
            Ingredients = await _inventoryService.GetAllIngredientsAsync();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)

            {
                Ingredients = await _inventoryService.GetAllIngredientsAsync();
                return Page();
            }
            await _inventoryService.CreateIngredientAsync(Ingredient);

            return RedirectToPage("/Admin/AllIngredients");

        }
            //var inventoryItem = new InventoryItem
            //{
            //    Ware = Ingredient,
            //    QuantityInStock = QuantityInStock
            //};

           
        
    }
}