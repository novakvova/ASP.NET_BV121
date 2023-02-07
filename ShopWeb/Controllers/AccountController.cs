using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public AccountController(UserManager<UserEntity> userManager)
        {
            _userManager= userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
