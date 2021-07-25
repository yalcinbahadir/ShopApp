using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApp.Entities
{
    public class Product
    {
        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }   
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ImgUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }       
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
