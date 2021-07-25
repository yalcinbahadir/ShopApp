using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.BussinessLayer.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using ShopAppTekrar.Data;
using ShopAppTekrar.Identity;
using ShopAppTekrar.Models;

namespace ShopAppTekrar.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //Closed after the implementation of bussiness layer
        //private IProductRepository _productRepository;
        //private ICategoryRepository _categoryRepository;
        private IProductServices _productServices;
        private ICategoryServices _categoryServices;
        private  RoleManager<IdentityRole> _roleManager;
        private  UserManager<User> _userManager;

        public AdminController(
            //IProductRepository productRepository, 
            //ICategoryRepository categoryRepository,
            RoleManager<IdentityRole> roleManager,
            IProductServices productServices,
            ICategoryServices categoryServices,
            UserManager<User> userManager
            )
        {
            //_productRepository = productRepository;
            //_categoryRepository = categoryRepository;
            _productServices = productServices;
            _categoryServices = categoryServices;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Product
        public IActionResult GetProducts()
        {
            var model = new ProductListModel()
            {
                Products = _productServices.GetAll(),
               
            };
            return View(model);
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var p = new Product() 
                { 
                     Name=model.Name,
                     Description=model.Description,
                     ImgUrl=model.ImgUrl,
                     IsApproved=model.IsApproved,
                     IsHome=model.IsHome,
                     Price=model.Price
                };
             _productServices.Create(p);
      
                return RedirectToAction("GetProducts");
            }
            return View(model);
        }
        public IActionResult UpdateProduct(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
           
            var entity = _productServices.GetByIdWithCategories((int)id);
            if (entity == null)
            {
                return NotFound();           
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImgUrl = entity.ImgUrl,
                Price = entity.Price,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(p => p.Category).ToList()
            };
            //var test = _categoryServices.GetAll();
            ViewBag.Categories = _categoryServices.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProduct(ProductModel model, int[] categoryIds)
        {
            if (ModelState.IsValid)
            {
                var entity = _productServices.GetById(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Description = model.Description;
                entity.ImgUrl = model.ImgUrl;
                entity.IsApproved = model.IsApproved;
                entity.IsHome = model.IsHome;

                _productServices.Update(entity, categoryIds);
                return RedirectToAction("GetProducts");
            }
            ViewBag.Categories = _categoryServices.GetAll();
            return View(model);
        }
        public IActionResult DeleteProduct(int id)
        {
            var entity = _productServices.GetById(id);
            if (entity != null)
            {
                _productServices.Delete(entity);
               
            }
            return RedirectToAction("GetProducts");
        }
        #endregion
        #region Roles
        public IActionResult ManageRoles()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result=await _roleManager.CreateAsync(new IdentityRole() { Name = model.Name });
                if (result.Succeeded)
                {
                    return RedirectToAction("GetProducts");
                }
            }
            return View(model);
        }
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> EditUserRoles(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var users = _userManager.Users;
            List<User> members = new List<User>();
            List<User> nonmembers = new List<User>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    members.Add(user);
                }
                else
                {
                    nonmembers.Add(user);
                }
            }

            var model = new RoleDetails()
            {
                 Role=role,
                 Members=members,
                 NonMembers=nonmembers
            };
            return View(model);
        }

        //RoleEditModel
        [HttpPost]
        //public async  Task<IActionResult> EditUserRoles(string roleName, string[] idsToDelete, string[] idsToAdd)
        public async Task<IActionResult> EditUserRoles(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("EditUserRoles","admin",new { roleId=model.RoleId });

            
        }


        #endregion
    }
}
