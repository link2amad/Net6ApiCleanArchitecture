using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
    {
        public void Configure(EntityTypeBuilder<ExceptionLog> entity)
        {
            entity.ToTable("ExceptionLog");

            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RequestUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}