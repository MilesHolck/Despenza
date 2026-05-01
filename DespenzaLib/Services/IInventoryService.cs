using DespenzaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Services
{
    public interface IInventoryService
    {

        //INVENTORYITEM
        Task<List<InventoryItem>> GetAllInventoryItemsAsync();
        Task CreateInventoryItemsAsync(InventoryItem item);
        Task<InventoryItem?> GetInventoryItemsByIdAsync(int id);
        Task UpdateInventoryItemsAsync(InventoryItem ingredient);        
        Task DeleteInventoryItemsAsync(int id);


        //INGREDIENT
        Task CreateIngredientAsync(Ingredient ingredient);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
        Task ReceiveIngredientAsync(int ingredientId, decimal amountInGrams);

        //SEMIPRODUCTS
        Task<List<SemiProduct>> GetAllSemiProductsAsync();
        Task CreateSemiProductAsync(SemiProduct semiProduct);


        //PRODUCTS
        Task<List<Product>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);

        //Waste
        Task RegisterWasteAsync(int wareId, string wareType, decimal quantity, string reason);
        Task<List<WasteRegistration>> GetAllWasteRegistrationsAsync();
        Task<List<InventoryItem>> GetInventoryItemsWithWareAsync();
    }
}
