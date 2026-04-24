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
        Task<List<Ingredients>> GetAllIngredientsAsync();
        Task CreateIngredientAsync(Ingredients ingredient);
        Task<Ingredients?> GetIngredientByIdAsync(int id);
        Task UpdateIngredientAsync(Ingredients ingredient);
        Task DeleteIngredientAsync(int id);

        Task<List<SemiProduct>> GetAllSemiProductsAsync();
        Task CreateSemiProductAsync(SemiProduct semiProduct);

        Task<List<Product>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);
    }
}
