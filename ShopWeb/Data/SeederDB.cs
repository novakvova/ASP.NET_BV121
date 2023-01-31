using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ShopWeb.Data.Entities;
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
            }
        }
    }
}
