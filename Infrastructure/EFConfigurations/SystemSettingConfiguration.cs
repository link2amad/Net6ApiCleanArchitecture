using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> entity)
        {
            entity.ToTable("SystemSettings");

            entity.Property(e => e.SettingName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.SettingKey)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.SettingValue)
               .IsRequired()
               .HasMaxLength(1000)
               .IsUnicode(false);

            entity.Property(e => e.SettingCategory)
              .HasMaxLength(50)
              .IsUnicode(false);

            entity.Property(e => e.Label)
              .HasMaxLength(255)
              .IsUnicode(false);

            entity.HasData(new SystemSetting
            {
                ID = 1,
                Active = true,
                Label = "From Mail",
                SettingCategory = "EmailSetting",
                SettingKey = "NotificationFromMailAddress",
                SettingName = "FromMail",
                SettingValue = "medteqreporteq@gmail.com"
            },
            new SystemSetting
            {
                ID = 2,
                Active = true,
                Label = "Smtp Client",
                SettingCategory = "EmailSetting",
                SettingKey = "SMTPClient",
                SettingName = "SmtpClient",
                SettingValue = "smtp.gmail.com"
            },
            new SystemSetting
            {
                ID = 3,
                Active = true,
                Label = "Smtp Port",
                SettingCategory = "EmailSetting",
                SettingKey = "Smtp Port",
                SettingName = "SmtpPort",
                SettingValue = "587"
            },
            new SystemSetting
            {
                ID = 4,
                Active = true,
                Label = "Smtp User",
                SettingCategory = "EmailSetting",
                SettingKey = "Smtp User Name",
                SettingName = "SmtpUser",
                SettingValue = "medteqreporteq@gmail.com"
            },
            new SystemSetting
            {
                ID = 5,
                Active = true,
                Label = "Smtp Password",
                SettingCategory = "EmailSetting",
                SettingKey = "Smtp Password",
                SettingName = "SmtpPassword",
                SettingValue = "medteqreporteq@1"
            },
            new SystemSetting
            {
                ID = 6,
                Active = true,
                Label = "URL Expiry Time",
                SettingCategory = "GeneralSetting",
                SettingKey = "Expiry Time",
                SettingName = "URLExpiryTime",
                SettingValue = "32"
            });
        }
    }
}