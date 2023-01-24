using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;
using ShopWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppEFContext _appEFContext;
        public CategoriesController(AppEFContext appEFContext)
        {
            _appEFContext = appEFContext;
            if(!_appEFContext.Categories.Any())
            {
                CategoryEntity cat = new CategoryEntity
                {
                    Name = "Ноутбуки",
                    DateCreated = DateTime.Now
                };
                _appEFContext.Categories.Add(cat);
                _appEFContext.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            var list = _appEFContext.Categories.ToList();
            return View(list);
        }
    }
}
