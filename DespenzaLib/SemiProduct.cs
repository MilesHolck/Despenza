using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class SemiProduct : Wares
    {
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public SemiProduct()
        {
            
        }

        public override string GetName()
        {
            return IngredientsName; 
        }

        public override double GetCost()
        {
            return KiloPrice; 
        }

        //public double AddIngredientPricesTogether()
        //{

        //}

    }
}
