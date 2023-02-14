using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskk.Entities;

namespace Taskk.Data;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .HasData(new System.Collections.Generic.List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Title = "HDD 1TB",
                    Quantity = 55,
                    Price = 74.09
                },
                new Product()
                {
                    Id = 2,
                    Title = "HDD SSD 512GB",
                    Quantity = 102,
                    Price = 190.99
                },
                new Product()
                {
                    Id = 3,
                    Title = "RAM DDR4 16GB",
                    Quantity = 47,
                    Price = 80.32
                }
            });

        base.OnModelCreating(builder);
    }
}