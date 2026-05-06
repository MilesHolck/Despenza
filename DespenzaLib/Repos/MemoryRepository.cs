using DespenzaLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class MemoryRepository<T> : IRepository<T> where T : class
    {

        protected readonly List<T> _items;
        private int _currentId = 1; 

        public MemoryRepository(InMemoryDb db)
        {
            if (!db.Sets.ContainsKey(typeof(T)))
            {
                db.Sets[typeof(T)] = new List<T>();
            }

            _items = (List<T>)db.Sets[typeof(T)]; 
        }

        public Task AddAsync(T item)
        {

            var property = typeof(T).GetProperty("Id"); 

            if (property != null)
            {
                var currentValue = (int)(property.GetValue(item) ?? 0);
                
                if(currentValue == 0)
                {
                    property.SetValue(item, _currentId);
                        _currentId++; 
                }
                        

                
            }
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(_items.ToList());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            var property = typeof(T).GetProperty("Id");

            if (property == null)
                return Task.FromResult<T?>(null);

            var item = _items.FirstOrDefault(x =>
                (int)(property.GetValue(x) ?? 0) == id);

            return Task.FromResult(item);
        }

        public Task UpdateAsync(T item)
        {
            var property = typeof(T).GetProperty("Id");
            var id = (int)(property!.GetValue(item)!); 

            for (int i = 0; i < _items.Count; i++)
            {
                var currentId = (int)(property.GetValue(_items[i])!); 

                if (currentId == id)
                {
                    _items[i] = item;
                    break;
                }
            }
            return Task.CompletedTask; 
        }

        public Task DeleteAsync(int id)
        {
            var property = typeof(T).GetProperty("Id");

            T? itemToRemove = null; 

            foreach(var item in _items)
            {
                var itemId = (int)(property.GetValue(item)!);
                
                if(itemId == id)
                {
                    itemToRemove = item;

                    break; 
                }
            }

            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove); 
            }

            return Task.CompletedTask; 
        }

        public IQueryable<T> GetQueryable()
        {
            // .AsQueryable() "snyder" C# til at behandle listen som en query
            return _items.AsQueryable();
        }
    }
}
