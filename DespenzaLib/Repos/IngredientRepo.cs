using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class IngredientRepo : IRepository<Ingredients>
    {

        public List<Ingredients> Collection { get; set; }

        

        public IngredientRepo()
        {
            Collection = new List<Ingredients>(); 
        }

        public void Add(Ingredients item)
        {
            Collection.Add(item); 
        }

        public List<Ingredients> GetAll()
        {
            return Collection;
        }

        public Ingredients Update(Ingredients item)
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

        public void Delete(Ingredients item)
        {
            Collection.Remove(item);     
        }

        public Ingredients GetById(int id)
        {
            return Collection.FirstOrDefault(i => i.Id == id);
        }




    }
}
