using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class Recipe 
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<RecipeLine> Lines { get; set; } = new List<RecipeLine>();

        public decimal OutputQuantity { get; set; } // i gram

        public Recipe() { }

        

    }
}
