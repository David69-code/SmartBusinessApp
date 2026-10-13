using Microsoft.EntityFrameworkCore;
using System.Configuration;
using SmartBusinessApp.Models;

namespace SmartBusinessApp.Data
{
    public class SmartBusinessContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SmartBusinessDB"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}