﻿using Microsoft.EntityFrameworkCore;
using Onion.Domain.Entities;
using System.Reflection;

namespace Onion.Persistance.Context;
public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
