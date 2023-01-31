using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;
using ShopWeb.Data.Entities;
using ShopWeb.Models.Categories;
using System;
using System.Collections.Generic;
using System.IO;
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
        //Для відображення вюшкі для додавання категорії
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //Для збереження даних на вьюшці додавання категорії
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            string imageName = String.Empty;
            if(model.UploadImage!=null)
            {
                string exp = Path.GetExtension(model.UploadImage.FileName);
                imageName = Path.GetRandomFileName()+ exp;
                string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), "images", imageName);
                using(var stream = System.IO.File.Create(dirSaveImage))
                {
                    await model.UploadImage.CopyToAsync(stream);
                }
            }
            CategoryEntity cat = new CategoryEntity
            {
                Name = model.Name,
                DateCreated = DateTime.Now,
                Image = imageName
            };
            _appEFContext.Categories.Add(cat);
            _appEFContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productEdit = _appEFContext.Categories.SingleOrDefault(c => c.Id == id);
            CategoryEditViewModel model = new CategoryEditViewModel
            {
                Name = productEdit.Name,
                Id = id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CategoryEditViewModel model) 
        {
            var edit = _appEFContext.Categories.SingleOrDefault(x=>x.Id== model.Id);
            edit.Name= model.Name;
            _appEFContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
