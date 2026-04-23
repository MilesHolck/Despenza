using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespenzaLib.Models;
using Microsoft.EntityFrameworkCore;


namespace DespenzaLib.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<SemiProduct> semiProducts { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }      
       


    }
}
