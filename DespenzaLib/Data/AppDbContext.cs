using Microsoft.EntityFrameworkCore;
using DespenzaLib.Models;
﻿using DespenzaLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DespenzaLib.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Apprentice> Apprentices { get; set; }
        public DbSet<Baker> Bakers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<SemiProduct> SemiProducts { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Apprentice>("Apprentice")
                .HasValue<Baker>("Baker");
        }
    }
}
