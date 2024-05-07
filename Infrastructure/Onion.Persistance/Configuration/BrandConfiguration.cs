using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Persistance.Configuration;
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(a => a.Name).HasMaxLength(250);

        Faker faker = new("tr");

        Brand brand1 = new()
        {
            Id = 1,
            Name = faker.Commerce.Department()
          
        };

        Brand brand2 = new()
        {
            Id = 2,
            Name = faker.Commerce.Department()           
        };

        Brand brand3 = new()
        {
            Id = 3,
            Name = faker.Commerce.Department(),          
            IsDeleted = true
        };

        builder.HasData(brand1, brand2, brand3);
    }
}
