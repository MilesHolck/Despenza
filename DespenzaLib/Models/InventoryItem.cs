using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class InventoryItem
    {

     
        public int Id { get; set; }

        public int WareId { get; set; }

        public Wares Ware { get; set; }

        public decimal QuantityInStock { get; set; }

        public DateOnly ExpirationDate { get; set; }

        public InventoryItem()
        {
            
        }


    }
}
