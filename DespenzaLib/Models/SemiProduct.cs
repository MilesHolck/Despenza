using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class SemiProduct : Wares
    {
       
        
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public User User { get; set; }


        public SemiProduct()
        {
            
        }

        public override decimal GetCost()
        {
            if (Recipe?.Lines == null || Recipe.OutputQuantity == 0)
            {
                return 0;
            }

            var totalCost = Recipe.Lines.Sum(l => l.Quantity * l.Ware.GetCost());

            return totalCost / Recipe.OutputQuantity; 
        }

            //public double AddIngredientPricesTogether()
            //{

            //}

        }
}
