using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfigurations
{
    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.ToTable("State");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Active);

            entity.HasData(new State
            {
                ID = 1,
                Name = "Alabama",
                Code = "AL",
                Active = true
            },
            new State
            {
                ID = 2,
                Name = "Alaska",
                Code = "AK",
                Active = true
            },
            new State
            {
                ID = 3,
                Name = "Arizona",
                Code = "AZ",
                Active = true
            },
            new State
            {
                ID = 4,
                Name = "Arkansas",
                Code = "AR",
                Active = true
            },
            new State
            {
                ID = 5,
                Name = "California",
                Code = "CA",
                Active = true
            },
            new State
            {
                ID = 6,
                Name = "Colorado",
                Code = "CO",
                Active = true
            },
            new State
            {
                ID = 7,
                Name = "Connecticut",
                Code = "CT",
                Active = true
            },
            new State
            {
                ID = 8,
                Name = "Delaware",
                Code = "DE",
                Active = true
            },
            new State
            {
                ID = 9,
                Name = "Florida",
                Code = "FL",
                Active = true
            },
            new State
            {
                ID = 10,
                Name = "Georgia",
                Code = "GA",
                Active = true
            },
            new State
            {
                ID = 11,
                Name = "Hawaii",
                Code = "HI",
                Active = true
            },
            new State
            {
                ID = 12,
                Name = "Idaho",
                Code = "ID",
                Active = true
            },
            new State
            {
                ID = 13,
                Name = "Illinois",
                Code = "IL",
                Active = true
            },
            new State
            {
                ID = 14,
                Name = "Indiana",
                Code = "IN",
                Active = true
            },
            new State
            {
                ID = 15,
                Name = "Iowa",
                Code = "IA",
                Active = true
            },
            new State
            {
                ID = 16,
                Name = "Kansas",
                Code = "KS",
                Active = true
            },
            new State
            {
                ID = 17,
                Name = "Kentucky",
                Code = "KY",
                Active = true
            },
            new State
            {
                ID = 18,
                Name = "Louisiana",
                Code = "LA",
                Active = true
            },
            new State
            {
                ID = 19,
                Name = "Maine",
                Code = "ME",
                Active = true
            },
            new State
            {
                ID = 20,
                Name = "Maryland",
                Code = "MD",
                Active = true
            },
            new State
            {
                ID = 21,
                Name = "Massachusetts",
                Code = "MA",
                Active = true
            },
            new State
            {
                ID = 22,
                Name = "Michigan",
                Code = "MI",
                Active = true
            },
            new State
            {
                ID = 23,
                Name = "Minnesota",
                Code = "MN",
                Active = true
            },
            new State
            {
                ID = 24,
                Name = "Mississippi",
                Code = "MS",
                Active = true
            },
            new State
            {
                ID = 25,
                Name = "Missouri",
                Code = "MO",
                Active = true
            },
            new State
            {
                ID = 26,
                Name = "Montana",
                Code = "MT",
                Active = true
            },
            new State
            {
                ID = 27,
                Name = "Nebraska",
                Code = "NE",
                Active = true
            },
            new State
            {
                ID = 28,
                Name = "Nevada",
                Code = "NV",
                Active = true
            },
            new State
            {
                ID = 29,
                Name = "New Hampshire",
                Code = "NH",
                Active = true
            },
            new State
            {
                ID = 30,
                Name = "New Jersey",
                Code = "NJ",
                Active = true
            },
            new State
            {
                ID = 31,
                Name = "New Mexico",
                Code = "NM",
                Active = true
            },
            new State
            {
                ID = 32,
                Name = "New York",
                Code = "NY",
                Active = true
            },
            new State
            {
                ID = 33,
                Name = "North Carolina",
                Code = "NC",
                Active = true
            },
            new State
            {
                ID = 34,
                Name = "North Dakota",
                Code = "ND",
                Active = true
            },
            new State
            {
                ID = 35,
                Name = "Ohio",
                Code = "OH",
                Active = true
            },
            new State
            {
                ID = 36,
                Name = "Oklahoma",
                Code = "OK",
                Active = true
            },
            new State
            {
                ID = 37,
                Name = "Oregon",
                Code = "OR",
                Active = true
            },
            new State
            {
                ID = 38,
                Name = "Pennsylvania[",
                Code = "PA",
                Active = true
            },
            new State
            {
                ID = 39,
                Name = "Rhode Island",
                Code = "RI",
                Active = true
            },
            new State
            {
                ID = 40,
                Name = "South Carolina",
                Code = "SC",
                Active = true
            },
            new State
            {
                ID = 41,
                Name = "South Dakota",
                Code = "SD",
                Active = true
            },
            new State
            {
                ID = 42,
                Name = "Tennessee",
                Code = "TN",
                Active = true
            },
            new State
            {
                ID = 43,
                Name = "Texas",
                Code = "TX",
                Active = true
            },
            new State
            {
                ID = 44,
                Name = "Utah",
                Code = "UT",
                Active = true
            },
            new State
            {
                ID = 45,
                Name = "Vermont",
                Code = "VT",
                Active = true
            },
            new State
            {
                ID = 46,
                Name = "Virginia",
                Code = "VA",
                Active = true
            },
            new State
            {
                ID = 47,
                Name = "Washington",
                Code = "WA",
                Active = true
            },
            new State
            {
                ID = 48,
                Name = "West Virginia",
                Code = "WV",
                Active = true
            },
            new State
            {
                ID = 49,
                Name = "Wisconsin",
                Code = "WI",
                Active = true
            },
            new State
            {
                ID = 50,
                Name = "Wyoming",
                Code = "WY",
                Active = true
            });
        }
    }
}