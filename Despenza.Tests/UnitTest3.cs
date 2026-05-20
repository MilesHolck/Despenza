using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despenza.Tests
{
    public class UnitTest3
    {


        [Fact]
        public async Task CanProduceAsync_ReturnsTrue_WhenEnoughInventoryItems()
        {

            var db = new InMemoryDb();

            var recipeRepo = new MemoryRepository<Recipe>(db);
            var inventoryRepo = new MemoryRepository<InventoryItem>(db);

            var recipe = new Recipe
            {
                Id = 1,
                Lines = new List<RecipeLine>
                {
                    new RecipeLine { WareId = 1, Quantity = 10}
                }
            };

            await recipeRepo.AddAsync(recipe);

            await inventoryRepo.AddAsync(new InventoryItem { WareId = 1, QuantityInStock = 20 });

            var service = new ProductionService(inventoryRepo, recipeRepo, null, null, null);

            //Act 
            var result = await service.CanProduceAsync(1);

            //Assert 
            Assert.True(result);

        }



        [Fact]

        public async Task CanProduceAsync_ReturnsFalse_WhenNotEnoughInventoryItem()
        {
            var db = new InMemoryDb();

            var recipeRepo = new MemoryRepository<Recipe>(db);

            var inventoryRepo = new MemoryRepository<InventoryItem>(db);

            var recipe = new Recipe
            {
                Id = 1,
                Lines = new List<RecipeLine>
                {
                    new RecipeLine { WareId = 1, Quantity = 10}
                }
            };

            await recipeRepo.AddAsync(recipe); 

            await inventoryRepo.AddAsync(new InventoryItem { WareId = 1, QuantityInStock = 5 });

            var service = new ProductionService(inventoryRepo, recipeRepo, null, null, null);

            //Act 
            var result = await service.CanProduceAsync(1);

            //Assert 
            Assert.False(result);

        }

    }
}
