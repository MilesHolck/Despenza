using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class Recipe 
    {
        public List<Wares> Components { get; set; } = new List<Wares>();

        protected Recipe() { }

        public void Add (Wares item)
        {
            this.Components.Add(item);
        }

    }
}
