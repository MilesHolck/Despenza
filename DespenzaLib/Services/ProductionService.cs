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
        private readonly IRepository<Ingredient> _ingredientRepo;
        private readonly IRepository<Product> _productRepo; 
        private readonly IRepository<SemiProduct> _semiProductRepo;

        public ProductionService(IRepository<InventoryItem> inventoryRepo, IRepository<Recipe> recipeRepo, IRepository<Ingredient> ingredientRepo, IRepository<Product> productRepo, IRepository<SemiProduct> semiProductRepo)
        {
            _inventoryRepo = inventoryRepo;
            _recipeRepo = recipeRepo;
            _ingredientRepo = ingredientRepo;
            _productRepo = productRepo;
            _semiProductRepo = semiProductRepo;        
        }

        public async Task<bool> CanProduceAsync(int recipeId)
        {
            var recipe = await _recipeRepo.GetByIdAsync(recipeId);

            if (recipe == null)
                return false;

            var inventory = await _inventoryRepo.GetAllAsync(); 

            foreach (var line in recipe.Lines)
            {
                var stockQuantity = inventory
                    .Where(i => i.WareId == line.WareId)
                    .Sum(i => i.QuantityInStock);

                if (stockQuantity < line.Quantity)
                    return false;
            }
            return true;

        }


        public async Task<decimal> CalculateSalesPriceAsync(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);

            if (product == null || product.Recipe == null)
            {
                return 0; 
            }

            var recipe = product.Recipe;

            decimal totalCost = 0; 

            foreach (var line in recipe.Lines)
            {
                totalCost += line.Quantity * line.Ware.GetCost(); 
            }

            decimal dBidrag = 0.70m;
            decimal VAT = 0.25m;

            decimal priceExVat = totalCost / (1 - dBidrag);
            decimal priceIncVat = priceExVat * (1 + VAT);

            return priceIncVat; 


        }
        
    }
}
