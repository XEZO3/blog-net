using blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace blog.Data
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { 
        
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=EZALDEEN;Database=Students;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
