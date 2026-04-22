using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class ProductRepo : IRepository<Product>
    {

        public List<Product> Collection { get; set; }

        public ProductRepo() { Collection = new List<Product>(); }

        public void Add(Product item)
        {
            Collection.Add(item); 
        }

        public void Delete(Product item)
        {
            Collection.Remove(item);
        }

        public List<Product> GetAll()
        {
            return Collection;
        }

        public Product GetById(int id)
        { return Collection.FirstOrDefault(i => i.Id == id); }
       

        public Product Update(Product item)
        {
            foreach (var i in Collection)
            {
                if (item.Id == i.Id)
                {
                    item.Name = i.Name;
                    item.KiloPrice = i.KiloPrice;

                    return i;

                }

            }
            return null;
        }
    }
}
