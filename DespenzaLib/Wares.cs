using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public abstract class Wares
    {
       public int Id { get; set; }

        public string Name { get; set; }

        public decimal KiloPrice { get; set; }



        public Wares() { }

  

        public abstract decimal GetCost(); 
        

    }


}
