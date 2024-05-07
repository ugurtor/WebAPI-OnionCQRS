using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Persistance.Configuration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        Category cat1 = new()
        {
            Id = 1,
            Name = "Elektrik",
            Priority = 1,
            ParentId = 0          
        };

        Category cat2 = new()
        {
            Id = 2,
            Name = "Moda",
            Priority = 2,
            ParentId = 0           
        };

        Category par1 = new()
        {
            Id = 3,
            Name = "Bilgisayar",
            Priority = 1,
            ParentId = 1
        };

        Category par2 = new()
        {
            Id = 4,
            Name = "Kadın",
            Priority = 1,
            ParentId = 2            
        };

        builder.HasData(cat1, cat2, par1, par2);
    }
}
