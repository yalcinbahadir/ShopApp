using ShopApp.BussinessLayer.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.BussinessLayer.Concrete
{
    public class ProductManager : IProductServices
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public bool Delete(Product entity)
        {
            return _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public List<Product> GetByCategory(string category)
        {
            return _productRepository.GetByCategory(category);
        }

        public List<Product> GetByCategoryId(int id)
        {
            return _productRepository.GetByCategoryId(id);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

        public List<Product> GetHomepageProducts()
        {
            return _productRepository.GetHomepageProducts();
        }

        public bool Update(Product entity)
        {
            return _productRepository.Update(entity);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productRepository.Update(entity, categoryIds);
        }
    }
}
