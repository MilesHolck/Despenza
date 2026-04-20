using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class WareCombination : Wares
    {
        public List<Wares> Components { get; set; } = new List<Wares>();

        protected WareCombination() { }

        public void Add (Wares item)
        {
            this.Components.Add(item);
        }

        public override double GetCost()
        {
            return KiloPrice; 
        }

        public override string GetName()
        {
            return IngredientsName; 
        }
    }
}
