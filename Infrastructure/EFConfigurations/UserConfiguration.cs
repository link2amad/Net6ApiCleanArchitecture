using Application.Helper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.EFConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.PasswordRequestDate).HasColumnType("datetime");

            entity.Property(e => e.PasswordRequestHash)
                .HasMaxLength(1500)
                .IsUnicode(false);

            //entity.HasOne(d => d.CreatedByUser)
            //    .WithMany(p => p.InverseCreatedByUser)
            //    .HasForeignKey(d => d.CreatedBy)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_User_User");

            //entity.HasOne(d => d.ModifiedByUser)
            //    .WithMany(p => p.InverseModifiedByUser)
            //    .HasForeignKey(d => d.ModifiedBy)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_User_User1");

            entity.HasData(new User
            {
                ID = 1,
                Active = true,
                Email = "admin@mail.com",
                FirstName = "Adam",
                LastName = "Admin",
                IsDeleted = false,
                Password = "test123".WithBCrypt(),
                CreatedDate = new DateTime(2023, 6, 1, 11, 23, 53, 353, DateTimeKind.Utc).AddTicks(7581),
                ModifiedDate = new DateTime(2023, 6, 1, 11, 23, 53, 353, DateTimeKind.Utc).AddTicks(7581),
            });
        }
    }
}