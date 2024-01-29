using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ParfumEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParfumEcommerce.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Parfums.Any())
                {
                    var categories = new List<Categorie>
                    {
                        new Categorie { CategoryName = "Bergamote Orange Dihydromyrcénol" },
                        new Categorie { CategoryName = "Cardamome" },
                        new Categorie { CategoryName = "Mandarine Curcuma" },
                        new Categorie { CategoryName = "Ambre" }
                    };

                    context.Categories.AddRange(categories);
                    context.SaveChanges();

                    var parfums = new List<Parfum>
                    {
                        new Parfum
                        {
                            ParfumName = "Sauvage",
                            Prix = 500,
                            Image = "img/sauvage-dior.jpg",
                            CategorieId = categories.First(c => c.CategoryName == "Bergamote Orange Dihydromyrcénol").ID
                        },
                        new Parfum
                        {
                            ParfumName = "Azzaro",
                            Prix = 750,
                            Image = "img/azzaro-the-most-wanted-eau-de-parfum-intense.jpg",
                            CategorieId = categories.First(c => c.CategoryName == "Cardamome").ID
                        },
                        new Parfum
                        {
                            ParfumName = "Lacoste",
                            Prix = 600,
                            Image = "img/lacoste-l-12-12-blanc-intense.jpg",
                            CategorieId = categories.First(c => c.CategoryName == "Mandarine Curcuma").ID
                        },
                        new Parfum
                        {
                            ParfumName = "Stronger With You Amber Armani",
                            Prix = 650,
                            Image = "img/stronger-with-you-amber.jpg",
                            CategorieId = categories.First(c => c.CategoryName == "Ambre").ID
                        },
                        new Parfum
                        {
                            ParfumName = "Jean Paul",
                            Prix = 950,
                            Image = "img/1.jpg",
                            CategorieId = categories.First(c => c.CategoryName == "Ambre").ID
                        }
                    };

                    context.Parfums.AddRange(parfums);
                    context.SaveChanges();
                }
            }
        }
    }
}
