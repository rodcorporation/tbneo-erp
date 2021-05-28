using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TbNeo.Domain.Entities;

namespace TbNeo.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("Usuario");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(p => p.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasIndex(p => p.Email)
                .IsUnique();

            builder
                .Property(p => p.Senha)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
