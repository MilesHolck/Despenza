using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib
{
    public class Product 
    {
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public Product()
        {
            
        }


    }
}
