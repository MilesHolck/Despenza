using Despenza.Pages;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;


namespace Despenza.Tests
{
    public class UnitTest2
    {
        public class ProductionServiceTests
        {
            
            private readonly ProductionService _classTest;

           
            private readonly Mock<IRepository<InventoryItem>> _mockInventoryRepo;
            private readonly Mock<IRepository<Recipe>> _mockRecipeRepo;
            private readonly Mock<IRepository<Ingredient>> _mockIngredientRepo;
            private readonly Mock<IRepository<Product>> _mockProductRepo;
            private readonly Mock<IRepository<SemiProduct>> _mockSemiProductRepo;

            public ProductionServiceTests()
            {
                _mockInventoryRepo = new Mock<IRepository<InventoryItem>>();
                _mockRecipeRepo = new Mock<IRepository<Recipe>>();
                _mockIngredientRepo = new Mock<IRepository<Ingredient>>();
                _mockProductRepo = new Mock<IRepository<Product>>();
                _mockSemiProductRepo = new Mock<IRepository<SemiProduct>>();

                
                _classTest = new ProductionService(
                    _mockInventoryRepo.Object,
                    _mockRecipeRepo.Object,
                    _mockIngredientRepo.Object,
                    _mockProductRepo.Object,
                    _mockSemiProductRepo.Object
                );
            }

            [Fact]
            public void CalculateRawMaterialCost_RecipeIsNull_ReturnsZero()
            {
                
                var result = _classTest.CalculateRawMaterialCost(null!);

               
                Assert.Equal(0m, result);
            }



            [Fact]
            public void CalculateRawMaterialCost_RecipeLinesAreNull_ReturnsZero()
            {
                // Arrange
                var recipe = new Recipe { Lines = null! };

                // Act
                var result = _classTest.CalculateRawMaterialCost(recipe);

                // Assert
                Assert.Equal(0m, result);
            }

            [Fact]
            public void CalculateRawMaterialCost_RecipeLinesAreEmpty_ReturnsZero()
            {
                // Arrange
                var recipe = new Recipe { Lines = new List<RecipeLine>() }; 

                // Act
                var result = _classTest.CalculateRawMaterialCost(recipe);

                // Assert
                Assert.Equal(0m, result);
            }

        }


    }
}