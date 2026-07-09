using Microsoft.EntityFrameworkCore;
using RSConnect.API.Models;

namespace RSConnect.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios"); // <-- TEM QUE SER MINÚSCULO

                entity.HasKey(u => u.id);

                entity.Property(u => u.id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.nome)
                      .HasColumnName("nome")
                      .IsRequired();

                entity.Property(u => u.email)
                      .HasColumnName("email")
                      .IsRequired();

                entity.Property(u => u.Senha)
                      .HasColumnName("senha")
                      .IsRequired();
            });
        }
    }
}
