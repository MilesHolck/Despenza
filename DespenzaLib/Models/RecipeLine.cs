using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class RecipeLine
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int WareId { get; set; }

        public Wares Ware { get; set; }

        public decimal Quantity { get; set; }

        public bool IsChecked { get; set; } = false;



    }
}
