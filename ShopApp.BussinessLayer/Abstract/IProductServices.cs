using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.BussinessLayer.Abstract
{
    public interface IProductServices
    {
        //Following methods will be implemendted in ProductManager class
        //Methods of IRepository
        Product GetById(int id);
        List<Product> GetAll();
        void Create(Product entity);
        bool Update(Product entity);
        bool Delete(Product entity);
        //Methods of IProductRepository
        List<Product> GetHomepageProducts();
        List<Product> GetByCategory(string category);
        List<Product> GetByCategoryId(int id);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}
