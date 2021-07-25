using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public class EFCOREProductRepository : EFCOREGenericRepository<Product, ShopContext>, IProductRepository
    {
        public List<Product> GetByCategory(string category)
        {
            /*select * from Products p 
             *                       join ProductCategories pc on p.Id=pc.ProductId 
                                     join Categories c on pc.CategoryId = c.Id 
                                     where c.Name = 'Electronic'
            */
            using (var context=new ShopContext())
            {
                var selectedProducts= context.Products.AsQueryable()
                    .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.Category)
                    .Where(p => p.ProductCategories.Any(p => p.Category.Name == category)).ToList();
                return selectedProducts;
            }
        }

        public List<Product> GetByCategoryId(int id)
        {
            using (var context = new ShopContext())
            {
                var selectedProducts = context.Products.AsQueryable()
                    .Include(p => p.ProductCategories)
                    .ThenInclude(p => p.Category)
                    .Where(p => p.ProductCategories.Any(p => p.Category.Id == id)).ToList();
                return selectedProducts;
            }
        }

        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                        .Where(i => i.Id == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .FirstOrDefault();
            }
        }

        public List<Product> GetHomepageProducts()
        {
            using (var context=new ShopContext())
            {
                return context.Products.Where(p => p.IsHome == true).ToList();
            }
            
        }

        public void Update(Product entity, int[] categoryIds)
        {
            //using (var context=new ShopContext())
            //{
            //    foreach (var categoryId in categoryIds)
            //    {
            //        var productCategory = new ProductCategory()
            //        {
            //            CategoryId = categoryId,
            //            ProductId = entity.Id
            //        };
            //        context.ProductCategories.Add(productCategory);
            //        entity.ProductCategories.Add(productCategory);
            //        context.SaveChanges();
            //    }
            //}
            using (var context = new ShopContext())
            {
                var product = context.Products
                                   .Include(i => i.ProductCategories)
                                   .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImgUrl = entity.ImgUrl;
                    product.Price = entity.Price;
                    product.IsApproved = entity.IsApproved;
                    product.IsHome = entity.IsHome;
                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId = catid,
                        ProductId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}
