using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class SemiProduct : Wares
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }


        public SemiProduct()
        {
            
        }

        public override decimal GetCost()
        {
            return Recipe.Lines.Sum(l => l.Quantity * l.Ware.GetCost());
        }

            //public double AddIngredientPricesTogether()
            //{

            //}

        }
}
