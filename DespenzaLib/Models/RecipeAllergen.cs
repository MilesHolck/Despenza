using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Models
{
    public class RecipeAllergen
    {

        public int Id { get; set; }
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public Allergen Allergen { get; set; }

    }
}
