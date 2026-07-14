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
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }

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

