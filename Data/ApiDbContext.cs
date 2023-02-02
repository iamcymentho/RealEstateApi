using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;
using System.ComponentModel;

namespace RealEstateApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=RealEstateDb");
        }
    }
}
