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
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

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

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("servicos");

                entity.HasKey(s => s.id);

                entity.Property(s => s.id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(s => s.clienteId)
                      .HasColumnName("cliente_id")
                      .IsRequired();

                entity.Property(s => s.prestadorId)
                      .HasColumnName("prestador_id");

                entity.Property(s => s.categoria)
                      .HasColumnName("categoria")
                      .IsRequired();

                entity.Property(s => s.descricao)
                      .HasColumnName("descricao")
                      .IsRequired();

                entity.Property(s => s.endereco)
                      .HasColumnName("endereco")
                      .IsRequired();

                entity.Property(s => s.dataHora)
                      .HasColumnName("data_hora")
                      .IsRequired();

                entity.Property(s => s.status)
                      .HasColumnName("status")
                      .IsRequired();
            });

            modelBuilder.Entity<Prestador>(entity =>
            {
                entity.ToTable("prestadores");

                entity.HasKey(p => p.id);

                entity.Property(p => p.id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.nome)
                      .HasColumnName("nome")
                      .IsRequired();

                entity.Property(p => p.email)
                      .HasColumnName("email")
                      .IsRequired();

                entity.Property(p => p.senha)
                      .HasColumnName("senha")
                      .IsRequired();

                entity.Property(p => p.telefone)
                      .HasColumnName("telefone")
                      .IsRequired();

                entity.Property(p => p.categoria)
                      .HasColumnName("categoria")
                      .IsRequired();

                entity.Property(p => p.endereco)
                      .HasColumnName("endereco")
                      .IsRequired();

                entity.Property(p => p.nota)
                      .HasColumnName("nota");
            });
        }
    }
}
