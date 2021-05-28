using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TbNeo.Domain.Entities;

namespace TbNeo.Data.Mappings
{
    public class ProjetoMapping : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder
                .ToTable("Projeto");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(p => p.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder
                .Property(p => p.Descricao)
                .HasColumnType("varchar(500)");

            builder
                .Property(p => p.UrlJira)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .HasOne(p => p.CriadoPor)
                .WithMany()
                .HasForeignKey(p => p.IdCriadoPor)
                .IsRequired();

            builder
                .HasOne(p => p.AtualizadoPor)
                .WithMany()
                .HasForeignKey(p => p.IdAtualizadoPor);
        }
    }
}
