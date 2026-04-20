using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class InventoryItem
    {

        public int Id { get; set; }

        public int WareId { get; set; }

        public Wares Ware { get; set; }

        public double QuantityInStock { get; set; }

        public DateOnly ExpirationDate { get; set; }


    }
}
