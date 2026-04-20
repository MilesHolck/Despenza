using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public abstract class Wares
    {
        public string IngredientsName { get; set; } 
        public double KiloPrice { get; set; }
        public double Quantity { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public double Unit { get; set; }
        public string Allergen { get; set; }


        public Wares() { }

        public abstract string GetName();

        public abstract double GetCost(); 
        

    }


}
