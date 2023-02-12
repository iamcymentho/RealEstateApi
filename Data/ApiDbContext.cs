using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;
using System.ComponentModel;

namespace RealEstateApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

       // public DbSet<Property> Properties { get; set; }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=RealEstateDb");
        }
    }
}
