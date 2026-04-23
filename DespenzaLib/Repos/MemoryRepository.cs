using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class MemoryRepository<T> : IRepository<T> where T : class
    {

        protected readonly List<T> _items = new List<T>(); 
        public void Add(T item)
        {
            _items.Add(item); 
        }

        public void Delete(T item)
        {
            _items.Remove(item);
            
        }

        public List<T> GetAll()
        {
            return _items; 
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(x =>
           (int)x.GetType().GetProperty("Id").GetValue(x) == id);
        }

        public T Update(T item)
        {
            var id = (int)item.GetType().GetProperty("Id").GetValue(item);

            var existing = GetById(id);
            if (existing == null) return null;

            _items.Remove(existing);
            _items.Add(item);

            return item;
        }
    }
}
