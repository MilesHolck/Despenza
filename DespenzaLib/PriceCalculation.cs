using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public abstract class PriceCalculation
    {
        //public Product RawMaterialsCost { get; set; }
        public decimal VAT { get; set; }
        public decimal Profitpercent { get; set; }

        public PriceCalculation()
        {
        }

        //public void CalculateFinalPrice(Product product)
        //{
           
        //} 
    }
}
