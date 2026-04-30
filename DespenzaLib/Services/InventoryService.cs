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
        private readonly IRepository<WasteRegistration> _wasteRepository;
        private readonly AppDbContext _context;

        public InventoryService(IRepository<InventoryItem> inventoryItemRepository, IRepository<Ingredient> ingredientRepository, IRepository<SemiProduct> semiProductRepository, IRepository<Product> productRepository, IRepository<WasteRegistration> wasteRepository, AppDbContext context)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _ingredientRepository = ingredientRepository;
            _semiProductRepository = semiProductRepository;
            _productRepository = productRepository;
            _wasteRepository = wasteRepository;
            _context = context;
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

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetByIdAsync(id); 
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.UpdateAsync(ingredient); 
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _ingredientRepository.DeleteAsync(id); 
        }

        public async Task RegisterWasteAsync(int wareId, string wareType, decimal quantity, string reason)
        {
            var inventoryItems = await _inventoryItemRepository.GetAllAsync();

            var inventoryItem = inventoryItems
                .FirstOrDefault(i => i.WareId == wareId);

            if (inventoryItem == null)
            {
                throw new Exception("Varen findes ikke på lager.");
            }

            if (inventoryItem.QuantityInStock < quantity)
            {
                throw new Exception("Der er ikke nok på lager til at registrere denne mængde som spild.");
            }

            inventoryItem.QuantityInStock -= quantity;

            decimal lossInCost = 0;

            if (inventoryItem.Ware is Ingredient ingredient)
            {
                lossInCost = ingredient.PricePerGram * quantity;
            }
            else
            {
                lossInCost = inventoryItem.Ware.GetCost() * quantity;
            }

            var wasteRegistration = new WasteRegistration
            {
                WareId = wareId,
                Ware = inventoryItem.Ware,
                WareType = wareType,
                Quantity = quantity,
                Unit = wareType == "Ingredient" ? "gram" : "stk",
                Reason = reason,
                LossInCost = lossInCost,
                RegisteredAt = DateTime.Now
            };

            await _inventoryItemRepository.UpdateAsync(inventoryItem);
            await _wasteRepository.AddAsync(wasteRegistration);
        }

        public async Task<List<WasteRegistration>> GetAllWasteRegistrationsAsync()
        {
            return await _context.WasteRegistrations
                .Include(w => w.Ware)
                .OrderByDescending(w => w.RegisteredAt)
                .ToListAsync();
        }

        public async Task<List<InventoryItem>> GetAllInventoryItemsWithWareAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Ware)
                .ToListAsync();
        }

        public async Task<List<InventoryItem>> GetInventoryItemsWithWareAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Ware)
                .ToListAsync();
        }
    }
}
