using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> entity)
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasData(
                new Gender
                {
                    ID = 1,
                    Value = "Male",
                    Active = true
                },
                new Gender
                {
                    ID = 2,
                    Value = "Female",
                    Active = true
                },
                new Gender
                {
                    ID = 3,
                    Value = "Other",
                    Active = true
                }
            );
        }
    }
}