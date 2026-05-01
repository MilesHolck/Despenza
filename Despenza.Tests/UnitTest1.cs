using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Moq;
using Xunit;

namespace Despenza.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CalculateSalesPrice_ReturnsCorrectPrice()
        {
            // Arrange
            var mockInventoryRepo = new Mock<IRepository<InventoryItem>>();
            var mockRecipeRepo = new Mock<IRepository<Recipe>>();
            var mockIngredientRepo = new Mock<IRepository<Ingredient>>();
            var mockProductRepo = new Mock<IRepository<Product>>();
            var mockSemiProductRepo = new Mock<IRepository<SemiProduct>>();

            var ingredient = new Ingredient
            {
                KiloPrice = 100
            };

            var recipeLine = new RecipeLine
            {
                Quantity = 1000,
                Ware = ingredient
            };

            var recipe = new Recipe
            {
                Lines = new List<RecipeLine> { recipeLine }
            };

            var product = new Product
            {
                Recipe = recipe
            };

            mockProductRepo
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(product);

            var service = new ProductionService(
                mockInventoryRepo.Object,
                mockRecipeRepo.Object,
                mockIngredientRepo.Object,
                mockProductRepo.Object,
                mockSemiProductRepo.Object);

            // Act
            var result = await service.CalculateSalesPriceAsync(1);

            // Assert
            //Assert.True(result > 0);
            Assert.Equal(416.67m, result, 2);
        }
    }
}
