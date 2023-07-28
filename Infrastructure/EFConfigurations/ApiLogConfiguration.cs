using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class ApiLogConfiguration : IEntityTypeConfiguration<ApiLog>
    {
        public void Configure(EntityTypeBuilder<ApiLog> entity)
        {
            entity.ToTable("ApiLog");

            entity.Property(e => e.RequestURL)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.RequestByURL)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.IPAddress)
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}