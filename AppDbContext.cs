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

        // Tabelas do sistema
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais (se precisar)
            // Exemplo: chave composta, relacionamentos, constraints etc.
        }
    }
}
