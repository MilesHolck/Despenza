using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class Ingredients : Wares
    {

        public decimal GramPriceResult => KiloPrice / 1000m;
        public Ingredients() : base() { }

        public override double GetCost()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
