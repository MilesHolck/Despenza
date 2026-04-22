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
        public List<SemiProduct> SemiProducts;
        public List<Product> Products;
        public WareRepository() { }

        public void AddIngredient(Ingredients ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void GetAllIngredients()
        {
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine(ingredient.Name);
            }
        }

        public void RemoveIngredient(Ingredients ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void UpdateIngredient(Ingredients ingredient, string newName)
        {
            ingredient.Name = newName;
        }

        public void AddSemiProduct(SemiProduct semiProduct)
        {
            SemiProducts.Add(semiProduct);
        }

        public void GetAllSemiproducts() 
        {
            foreach (var Semiproduct in SemiProducts)
                throw new NotImplementedException();
        }

        public void RemoveSemiProduct(SemiProduct semiProduct)
        {
            SemiProducts.Remove(semiProduct);
        }

        public void UpdateSemiProduct(SemiProduct semiProduct/* string newName, whatever vi gerne vil have edited*/)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
        public void RemoveProduct(Product product) 
        { 
            Products.Remove(product);   
        }

        public void UpdateProduct(Product product /*string newName, whatever vi gerne vil have edited*/)
        {
            throw new NotImplementedException();
        }

        public void GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
