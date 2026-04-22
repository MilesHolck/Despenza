using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    internal class RecipeRepo : IRepository<Recipe>
    {

        public List<Recipe> Collection {  get; set; }

        public RecipeRepo()
        {
            Collection = new List<Recipe>();
        }

        public void Add(Recipe item)
        {
            Collection.Add(item); 
        }

        public void Delete(Recipe item)
        {
            Collection.Remove(item); 
        }

        public List<Recipe> GetAll()
        {
            return Collection; 
        }

        public Recipe GetById(int id)
        {
            return Collection.FirstOrDefault(i => i.Id == id); 
        }

        public Recipe Update(Recipe item)
        {
            foreach (var i in Collection)
            {
                if (item.Id == i.Id)
                {
                    item.Name = i.Name;
                   

                    return i;

                }

            }
            return null;
        }
    }
}
