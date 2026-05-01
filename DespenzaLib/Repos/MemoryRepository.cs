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
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(_items);
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
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            return Task.CompletedTask;
        }

        public IQueryable<T> GetQueryable()
        {
            // .AsQueryable() "snyder" C# til at behandle listen som en query
            return _items.AsQueryable();
        }
    }
}
