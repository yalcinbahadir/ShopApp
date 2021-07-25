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
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        private IProductServices _productServices;
        public HomeController(IProductRepository productRepository, IProductServices productServices)
        {
            _productRepository = productRepository;
            _productServices = productServices;
        }
        public IActionResult Index()
        {
            //var products = DataManager.products;
            //var categories = DataManager.categories;
            //var homeProducts = _productRepository.GetHomepageProducts();
            var homeProducts = _productServices.GetHomepageProducts();
            

            ProductCategoryModel model = new ProductCategoryModel()
            {
                Products = homeProducts,
                
            };

   

            return View(model);
        }
    }
}
