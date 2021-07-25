using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAppTekrar.Identity;
using ShopAppTekrar.Models;

namespace ShopAppTekrar.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName=model.UserName,
                    Email=model.Email,
                    
                };
                var result=await _userManager.CreateAsync(user, model.Password);
              
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "customer");
                    return RedirectToAction("index", "home");
                }
            }
            return View(model);
        }
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginModel() {ReturnUrl=returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);               
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                ModelState.AddModelError("", "No mathcing email.");
                return View(model);
            }
            var result= await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                if (model.ReturnUrl!=null)
                {
                    return RedirectToAction(model.ReturnUrl);
                }
                return RedirectToAction("index", "home");
            }
            ModelState.AddModelError("", "Username or password is not correct.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
