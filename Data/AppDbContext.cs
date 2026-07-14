using Microsoft.EntityFrameworkCore;
using RSConnect.API.Models;

namespace RSConnect.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    id = 1,
                    nome = "Admin",
                    email = "admin@test.com",
                    senha = "123456"
                }
            );
        }
    }
}
