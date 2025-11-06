using Microsoft.EntityFrameworkCore;
using windows_pos_system.Models;

namespace windows_pos_system.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pos.db");
        }
    }
}