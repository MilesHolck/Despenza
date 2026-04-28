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
        Task<List<InventoryItem>> GetAllInventoryItemsAsync();
        Task CreateIngredientAsync(Ingredient ingredient);
        Task CreateInventoryItemsAsync(InventoryItem item);
        Task<InventoryItem?> GetInventoryItemsByIdAsync(int id);
        Task UpdateInventoryItemsAsync(InventoryItem ingredient);
        Task DeleteInventoryItemsAsync(int id);

        Task<List<SemiProduct>> GetAllSemiProductsAsync();
        Task CreateSemiProductAsync(SemiProduct semiProduct);

        Task<List<Product>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);
        Task<List<Ingredient>> GetAllIngredientsAsync();
    }
}
