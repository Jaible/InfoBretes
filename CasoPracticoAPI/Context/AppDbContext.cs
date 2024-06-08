using CasoPracticoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CasoPracticoAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
        }

        public DbSet<User> Users { get; set; }
    }
}
