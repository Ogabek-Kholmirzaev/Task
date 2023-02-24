using Microsoft.AspNetCore.Identity;
using Taskk.Entities;
using Taskk.Statics;

namespace Taskk.Data;

public static class AppDbSeedData
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();
            
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        Title = "HDD 1TB",
                        Quantity = 55,
                        Price = 74.09
                    },
                    new Product()
                    {
                        Title = "HDD SSD 512GB",
                        Quantity = 102,
                        Price = 190.99
                    },
                    new Product()
                    {
                        Title = "RAM DDR4 16GB",
                        Quantity = 47,
                        Price = 80.32
                    }
                });

                context.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var adminUserName = "admin";
            var adminUser = await userManager.FindByNameAsync(adminUserName);

            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    FullName = "Admin",
                    UserName = adminUserName,
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newAdminUser, "11111111");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }


            var appUserUserName = "user";
            var appUser = await userManager.FindByNameAsync(appUserUserName);

            if (appUser == null)
            {
                var newAppUser = new AppUser()
                {
                    FullName = "Application User",
                    UserName = appUserUserName,
                    Email = "user@gmail.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newAppUser, "11111111");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }

    public static void SeedUsersAndRolesAsync()
    {
        throw new NotImplementedException();
    }
}