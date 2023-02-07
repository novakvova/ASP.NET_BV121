using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ShopWeb.Constants;
using ShopWeb.Data.Entities;
using ShopWeb.Data.Entities.Identity;
using System;

namespace ShopWeb.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using(var scope = 
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                context.Database.Migrate();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                if (!context.Categories.Any())
                {
                    CategoryEntity cat = new CategoryEntity
                    {
                        Name = "Піци",
                        DateCreated = DateTime.Now
                    };
                    context.Categories.Add(cat);
                    context.SaveChanges();
                }

                if(!context.Products.Any())
                {
                    ProductEntity product = new ProductEntity
                    {
                        Name = "Піца Маргарита",
                        DateCreated = DateTime.Now,
                        Price = 220,
                        CategoryId = 1,
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                    ProductEntity product2 = new ProductEntity
                    {
                        Name = "Піца Папероні",
                        DateCreated = DateTime.Now,
                        Price = 190,
                        CategoryId = 1,
                    };
                    context.Products.Add(product2);
                    context.SaveChanges();
                }
            
                if(!context.Roles.Any())
                {
                    RoleEntity admin = new RoleEntity
                    {
                        Name= Roles.Admin,
                    };
                    RoleEntity user = new RoleEntity
                    {
                        Name = Roles.User,
                    };
                    var result = roleManager.CreateAsync(admin).Result;
                    result = roleManager.CreateAsync(user).Result;
                }

                if (!context.Users.Any())
                {
                    UserEntity user = new UserEntity
                    {
                        FirstName= "Марко",
                        LastName="Муха",
                        Email="muxa@gmail.com",
                        UserName= "muxa@gmail.com",
                    };
                    var result = userManager.CreateAsync(user, "123456")
                        .Result;
                    if(result.Succeeded)
                    {
                        result = userManager
                            .AddToRoleAsync(user, Roles.Admin)
                            .Result;
                    }
                }
            }
        }
    }
}
