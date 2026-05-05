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
        public decimal QuantityOfProduct { get; set; }
        public decimal RecipeScale { get; set; }
        public bool IsSavedCopy { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public DateTime DateSaved { get; set; } = DateTime.Now;
        public List<RecipeLine> Lines { get; set; } = new List<RecipeLine>();
        public int? UserId { get; set; }
        public User? User { get; set; }
        public decimal OutputQuantity { get; set; }
        public Recipe() { }



    }
}
