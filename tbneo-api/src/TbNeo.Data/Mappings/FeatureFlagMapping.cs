using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TbNeo.Domain.Entities;

namespace TbNeo.Data.Mappings
{
    public class FeatureFlagMapping : IEntityTypeConfiguration<FeatureFlag>
    {
        public void Configure(EntityTypeBuilder<FeatureFlag> builder)
        {
            builder
                .ToTable("FeatureFlag");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasOne(p => p.Projeto)
                .WithMany(p => p.FeatureFlags)
                .HasForeignKey(p => p.IdProjeto)
                .IsRequired();
        }
    }
}
