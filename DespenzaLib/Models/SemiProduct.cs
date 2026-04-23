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


        public SemiProduct()
        {
            
        }

        public override decimal GetCost()
        {
            //Alle ? er for at prog ikke crasher hvis recipe er null. 

            return Recipe?.Lines?.Sum(l => l.Quantity * l.Ware.GetCost()) ?? 0;
        }

            //public double AddIngredientPricesTogether()
            //{

            //}

        }
}
