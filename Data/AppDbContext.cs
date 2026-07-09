protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Usuario>(entity =>
    {
        entity.ToTable("Usuarios"); // <-- CORRIGIDO

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
