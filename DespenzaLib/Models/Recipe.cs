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
        public string? Description { get; set; }
        public List<RecipeLine> Lines { get; set; } = new List<RecipeLine>();

        public decimal OutputQuantity { get; set; } // hvor meget får vi ud af opskriften i gram

        public Recipe() { }



    }
}
