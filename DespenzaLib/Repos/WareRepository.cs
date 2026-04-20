using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Repos
{
    public class WareRepository
    {
        public List<Ingredients> Ingredients;
        //public List<SemiProduct> SemiProducts;
        //public List<Product> Products;
        public WareRepository() { }

        public void AddIngredient(Ingredients ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void GetAllIngredients()
        {
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine(ingredient.IngredientsName);
            }
        }

        public void DeleteIngredient(Ingredients ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void UpdateIngredient(Ingredients ingredient, string newName)
        {
            ingredient.IngredientsName = newName;
        }   

        //public void AddSemiProduct(SemiProduct semiProduct)
        //{
        //    SemiProducts.Add(semiProduct);
        //}

        //public void AddProduct(Product product)
        //{
        //    Products.Add(product);
        //}
    }
}
