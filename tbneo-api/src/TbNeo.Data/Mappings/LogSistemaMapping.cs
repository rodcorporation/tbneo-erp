using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TbNeo.Domain.Entities;

namespace TbNeo.Data.Mappings
{
    public class LogSistemaMapping : IEntityTypeConfiguration<LogSistema>
    {
        public void Configure(EntityTypeBuilder<LogSistema> builder)
        {
            builder
                .ToTable("LogSistema");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.NomeCampo)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(p => p.ValorNovo)
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.ValorAntigo)
                .HasColumnType("varchar(100)");

            builder
                .HasOne(p => p.AlteradoPor)
                .WithMany()
                .HasForeignKey(p => p.IdAlteradoPor)
                .IsRequired();
        }
    }
}
