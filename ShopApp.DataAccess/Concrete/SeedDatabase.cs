using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public static class SeedDatabase
    {
        private static List<Category> categories = new List<Category>()
        {
                new Category(){ Name="Computer", Description="Compter description "},
                new Category(){  Name="Phone", Description="Phone description "},
                new Category(){  Name="Game", Description="Game description "},
                new Category(){  Name="Electronic", Description="Electronic description "}
        };
        private static List<Product> products = new List<Product>()
        {
            //Computers
                new Product(){ Name="Computer-1", Price=1100, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="1.png", IsApproved=true, IsHome=true },
                new Product(){ Name="Computer-2", Price=1200, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="2.png", IsApproved=true, IsHome=true },
                new Product(){ Name="Computer-3", Price=1300, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="3.png", IsApproved=true, IsHome=true },
                new Product(){ Name="Computer-4", Price=1400, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="4.png", IsApproved=true, IsHome=true },
                //phones
                new Product(){ Name="Phone-1", Price=100, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="1.jpg", IsApproved=true, IsHome=true },
                new Product(){ Name="Phone-2", Price=200, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="2.jpg", IsApproved=true, IsHome=true },
                new Product(){ Name="Phone-3", Price=300, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="3.jpg", IsApproved=true, IsHome=true },
                new Product(){ Name="Phone-4", Price=400, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="4.jpg", IsApproved=true, IsHome=true },
                //games
                new Product(){ Name="Game-1", Price=10, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="7.png", IsApproved=true, IsHome=true },
                new Product(){ Name="Game-2", Price=20, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="8.png", IsApproved=true, IsHome=true },
                new Product(){  Name="Game-3", Price=30, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="9.png", IsApproved=true, IsHome=true },
                new Product(){  Name="Game-4", Price=40, Description="Lorem ipsum dolor sit amet consectetur adipisicing elit.",  ImgUrl="10.png", IsApproved=true, IsHome=true },
        };

        private static List<ProductCategory> productCategories = new List<ProductCategory>()
        {
            new ProductCategory(){ Product= products[0], Category=categories[0]},
            new ProductCategory(){ Product= products[1], Category=categories[0]},
            new ProductCategory(){ Product= products[2], Category=categories[0]},
            new ProductCategory(){ Product= products[3], Category=categories[0]},

            new ProductCategory(){ Product= products[4], Category=categories[1]},
            new ProductCategory(){ Product= products[5], Category=categories[1]},
            new ProductCategory(){ Product= products[6], Category=categories[1]},
            new ProductCategory(){ Product= products[7], Category=categories[1]},

            new ProductCategory(){ Product= products[8], Category=categories[2]},
            new ProductCategory(){ Product= products[9], Category=categories[2]},
            new ProductCategory(){ Product= products[10], Category=categories[2]},
            new ProductCategory(){ Product= products[11], Category=categories[2]},

            //
            new ProductCategory(){ Product= products[0], Category=categories[3]},
            new ProductCategory(){ Product= products[1], Category=categories[3]},
            new ProductCategory(){ Product= products[2], Category=categories[3]},
            new ProductCategory(){ Product= products[3], Category=categories[3]},

            new ProductCategory(){ Product= products[4], Category=categories[3]},
            new ProductCategory(){ Product= products[5], Category=categories[3]},
            new ProductCategory(){ Product= products[6], Category=categories[3]},
            new ProductCategory(){ Product= products[7], Category=categories[3]},


        };
    
        public static void Seed()
        {
            using(var context =new ShopContext())
            {
                if (context.Database.GetPendingMigrations().Count()==0)
                {
                    if (context.Categories.Count()==0)
                    {
                        context.Categories.AddRange(categories);
                    }
                    if (context.Products.Count() == 0)
                    {
                        context.Products.AddRange(products);
                    }
                    context.SaveChanges();
                    if (context.ProductCategories.Count() == 0)
                    {
                        context.AddRange(productCategories);
                    }
                    context.SaveChanges();
                }
                
               
              
                
          

            };
        }
    
    
    }
}
