using Despenza.Pages;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Moq;
using Xunit;
using Despenza;




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
            var result = service.CalculateSalesPrice(recipe);

            // Assert
            //Assert.True(result > 0);
            Assert.Equal(416.67m, result, 2);


        }

        public class UnitTest2
        {
            [Theory]
            [InlineData(0.5)]
            [InlineData(1.0)] //standard skalering 
            [InlineData(1.5)]
            [InlineData(2.0)]
            [InlineData(3.0)]
            public async Task OnGetAsync_WithScale_ReturnsCorrectCalculation(decimal scaleToTest)
            {
                var mockRecipeRepo = new Mock<IRepository<Recipe>>();
                
               
                var testRecipe = new Recipe
                {
                    Id = 1,
                    IsSavedCopy = false,
                    QuantityOfProduct = 10m,
                    Lines = new List<RecipeLine>
            {
                new RecipeLine { Quantity = 5m }, 
                new RecipeLine { Quantity = 2m }  
            }
                };

                var fakeDatabaseList = new List<Recipe> { testRecipe }.AsQueryable();

                mockRecipeRepo.Setup(repo => repo.GetQueryable()).Returns(fakeDatabaseList);

                var pageModel = new CreateRecipeModel(mockRecipeRepo.Object, null, null, null);

                // Act
                await pageModel.OnGetAsync(1, scaleToTest);

                // Assert
                Assert.Equal(10m * scaleToTest, testRecipe.QuantityOfProduct);
                Assert.Equal(scaleToTest, testRecipe.RecipeScale);
                Assert.Equal(5m * scaleToTest, testRecipe.Lines.ElementAt(0).Quantity);
                Assert.Equal(2m * scaleToTest, testRecipe.Lines.ElementAt(1).Quantity);


            }

        }
    }
}
