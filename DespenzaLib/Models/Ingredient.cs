using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class Ingredient : Wares
    {
        public string Unit {  get; set; }
        public decimal PricePerGram => KiloPrice / 1000m;


        public Ingredient() : base() { }

        public override decimal GetCost()
        {
            return KiloPrice / 1000m; 
        }

       
    }
}
