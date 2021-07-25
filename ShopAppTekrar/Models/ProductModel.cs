using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAppTekrar.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(200,ErrorMessage ="Description can not be more than 200 characters.")]
        public string Description { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 0-10000.")]
        public decimal? Price { get; set; }
        [Required]
        [DisplayName("Product image URL")]
        public string ImgUrl { get; set; }
        [DisplayName("Is approved?")]
        public bool IsApproved { get; set; }
        [DisplayName("Is homepage product?")]
        public bool IsHome { get; set; }
       
        public List<Category> SelectedCategories { get; set; }
    }
}
