using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> entity)
        {
            entity.ToTable("Entity");

            entity.Property(e => e.EntityName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}