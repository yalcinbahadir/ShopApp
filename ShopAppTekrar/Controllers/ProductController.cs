using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.BussinessLayer.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopAppTekrar.Data;
using ShopAppTekrar.Models;

namespace ShopAppTekrar.Controllers
{
    public class ProductController : Controller
    {
        //Closed after the implementation of bussinesslayer
        //private IProductRepository _productRepository;
        //private ICategoryRepository _categoryRepository;
        private IProductServices _productServices;
        private ICategoryServices _categorySevices;
        public ProductController(//IProductRepository productRepository, 
                                 //ICategoryRepository categoryRepository,
                                 IProductServices productServices, 
                                 ICategoryServices categorySevices
                                 )
        {
            //_productRepository = productRepository;
            //_categoryRepository = categoryRepository;
            _productServices = productServices;
            _categorySevices = categorySevices;
        }

        public IActionResult ProductList()
        {
            //var products = _productRepository.GetAll();
            var products = _productServices.GetAll();
            //var categories = _categoryRepository.GetAll();
            var categories = _categorySevices.GetAll();
            ProductCategoryModel model = new ProductCategoryModel()
            {
                Products = products,
                Categories = categories
            };            
            return View(model);         
        }

        public IActionResult ProductListByCategory(int? categoryId)
        {
            //var products = _productRepository.GetAll();
            var products = _productServices.GetAll();
            if (categoryId !=null)
            {                         
                products = _productServices.GetByCategoryId((int)categoryId);                
            }            
            var categories = _categorySevices.GetAll();
            ProductCategoryModel model = new ProductCategoryModel()
            {
                Products = products,
                Categories = categories
            };
            return View("ProductList", model);
        }

        // productDetails
        public IActionResult ProductDetails(int id)
        {
            var products = _productServices.GetAll();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }
    }
}
