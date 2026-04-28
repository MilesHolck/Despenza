using DespenzaLib.Data;
using DespenzaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DespenzaLib.Repos;

namespace DespenzaLib.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<InventoryItem> _inventoryItemRepository;
        private readonly IRepository<Ingredient> _ingredientRepository;
        private readonly IRepository<SemiProduct> _semiProductRepository;
        private readonly IRepository<Product> _productRepository;

        public InventoryService(IRepository<InventoryItem> inventoryItemRepository, IRepository<Ingredient> ingredientRepository, IRepository<SemiProduct> semiProductRepository, IRepository<Product> productRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _ingredientRepository = ingredientRepository;
            _semiProductRepository = semiProductRepository;
            _productRepository = productRepository;
        }

        public async Task<List<InventoryItem>> GetAllInventoryItemsAsync()
        {
            return await _inventoryItemRepository.GetAllAsync(); 
        }

        public async Task CreateInventoryItemsAsync(InventoryItem item)
        {
            await _inventoryItemRepository.AddAsync(item);
        }

        public async Task<List<SemiProduct>> GetAllSemiProductsAsync()
        {
            return await _semiProductRepository.GetAllAsync();
        }

        public async Task CreateSemiProductAsync(SemiProduct semiProduct)
        {
            await _semiProductRepository.AddAsync(semiProduct);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task<InventoryItem?> GetInventoryItemsByIdAsync(int id)
        {
            return await _inventoryItemRepository.GetByIdAsync(id);
        }

        public async Task UpdateInventoryItemsAsync(InventoryItem ingredient)
        {
            await _inventoryItemRepository.UpdateAsync(ingredient);
        }

        public async Task DeleteInventoryItemsAsync(int id)
        {
            await _inventoryItemRepository.DeleteAsync(id);
        }
        public async Task <List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }

        public async Task CreateIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.AddAsync(ingredient);
        }
    }
}
