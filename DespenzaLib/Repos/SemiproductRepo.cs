using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class SemiproductRepo : IRepository<SemiProduct>
    {

        public List<SemiProduct> Collection { get; set; }


        public SemiproductRepo()
        {
            Collection = new List<SemiProduct>();
        }
        public void Add(SemiProduct item)
        {
            Collection.Add(item); 
        }

        public void Delete(SemiProduct item)
        {
            foreach (var i in Collection)
            {
                if(i.Id == item.Id)
                {
                    Collection.Remove(i); 

                }
            }
          
        }

        public List<SemiProduct> GetAll()
        {
           return Collection;
        }

        public SemiProduct GetById(int id)
        {
            return Collection.FirstOrDefault(i => i.Id == id);
        }

        public SemiProduct Update(SemiProduct item)
        {
           foreach (var i in Collection)
            {
                if(item.Id == i.Id)
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
