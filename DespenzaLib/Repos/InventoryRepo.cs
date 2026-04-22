using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class InventoryRepo : IRepository<InventoryItem>

    {

        public List<InventoryItem> Collection { get; set; }

        public InventoryRepo()
        {
            Collection = new List<InventoryItem>();
        }
        public void Add(InventoryItem item)
        {
            Collection.Add(item); 
        }

        public void Delete(InventoryItem item)
        {
            Collection.Remove(item); 
        }

        public List<InventoryItem> GetAll()
        {
            return Collection; 
        }

        public InventoryItem GetById(int id)
        {
            return Collection.FirstOrDefault(i => i.Id == id); 
        }

        public InventoryItem Update(InventoryItem item)
        {
            foreach (var i in Collection)
            {
                if ( i.Id == item.Id)
                {
                    i.Ware = item.Ware; 
                    i.QuantityInStock = item.QuantityInStock;

                    return i; 

                }
            }
            return null; 
        }
    }
}
