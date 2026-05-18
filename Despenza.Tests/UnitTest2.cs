using Despenza.Pages;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despenza.Tests
{
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
