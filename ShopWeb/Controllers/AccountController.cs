using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data.Entities.Identity;
using ShopWeb.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        public AccountController(UserManager<UserEntity> userManager, 
            SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl="/")
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Redirect(model.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "Дані вказано не вірно!");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Categories");
        }
    }
}
