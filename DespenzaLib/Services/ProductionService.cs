using DespenzaLib.Models;
using DespenzaLib.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Services
{
    public class ProductionService
    {

        private readonly IRepository<InventoryItem> _inventoryRepo;
        private readonly IRepository<Recipe> _recipeRepo;

        public ProductionService(IRepository<InventoryItem> inventoryRepo, IRepository<Recipe> recipeRepo)
        {
            _inventoryRepo = inventoryRepo;
            _recipeRepo = recipeRepo; 

        }

        public async Task<bool> CanProduceAsync(int recipeId)
        {
            var recipe = await _recipeRepo.GetByIdAsync(recipeId);

            if (recipe == null)
                return false;

            var inventory = await _inventoryRepo.GetAllAsync();

            foreach (var line in recipe.Lines)
            {
                var stock = inventory.FirstOrDefault(i => i.WareId == line.WareId);

                if (stock == null || stock.QuantityInStock < line.Quantity)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
