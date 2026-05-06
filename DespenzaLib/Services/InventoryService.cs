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
       
        public InventoryService(IRepository<InventoryItem> inventoryItemRepository, IRepository<Ingredient> ingredientRepository, IRepository<SemiProduct> semiProductRepository, IRepository<Product> productRepository, IRepository<WasteRegistration> wasteRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _ingredientRepository = ingredientRepository;
            _semiProductRepository = semiProductRepository;
            _productRepository = productRepository;
            _wasteRepository = wasteRepository;
            
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
            //return await _context.WasteRegistrations
            //    .Include(w => w.Ware)
            //    .OrderByDescending(w => w.RegisteredAt)
            //    .ToListAsync();

            //INCLUDE VIRKER KUN I EF PGA INHERITANCE. NEDENSTÅENDE KAN ERSTATTE OVENSTÅENDE METODE, MEN ER IKKE OPTIMALT: 

            var wasteList = await _wasteRepository.GetAllAsync();

            var ingredients = await _ingredientRepository.GetAllAsync();
            var semiProducts = await _semiProductRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync(); 



            foreach (var waste in wasteList)
            {
                waste.Ware = waste.WareType switch
                {
                    "Ingredient" => ingredients.FirstOrDefault(i => i.Id == waste.WareId),
                    "SemiProduct" => semiProducts.FirstOrDefault(s => s.Id == waste.WareId),
                    "Product" => products.FirstOrDefault(p => p.Id == waste.WareId),
                    _ => null
                }; 


            }

            return wasteList
                .OrderByDescending(w => w.RegisteredAt)
                .ToList(); 
        }

        

        public async Task<List<InventoryItem>> GetInventoryItemsWithWareAsync()
        {
            //return await _context.InventoryItems
            //    .Include(i => i.Ware)
            //    .ToListAsync();

            var inventoryItems = await _inventoryItemRepository.GetAllAsync();

            var ingredients = await _ingredientRepository.GetAllAsync();
            var semiProducts = await _semiProductRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync(); 

            foreach (var item in inventoryItems)
            {
                item.Ware = item.Ware switch
                {
                    Ingredient => ingredients.FirstOrDefault(i => i.Id == item.WareId),
                    SemiProduct => semiProducts.FirstOrDefault(s => s.Id == item.WareId),
                    Product => products.FirstOrDefault(p => p.Id == item.WareId),
                    _ => item.Ware
                }; 

             }
            return inventoryItems; 
        }

        public async Task ReceiveIngredientAsync(int ingredientId, decimal amountInGrams)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);

            if (ingredient == null)
            {
                throw new Exception("Ingrediensen findes ikke.");
            }

            var inventoryItems = await _inventoryItemRepository.GetAllAsync();

            var inventoryItem = inventoryItems
                .FirstOrDefault(i => i.WareId == ingredientId);

            if (inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    WareId = ingredientId,
                    Ware = ingredient,
                    QuantityInStock = amountInGrams,
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6))
                };

                await _inventoryItemRepository.AddAsync(inventoryItem);
            }
            else
            {
                inventoryItem.QuantityInStock += amountInGrams;

                await _inventoryItemRepository.UpdateAsync(inventoryItem);
            }
        }
    }
}
