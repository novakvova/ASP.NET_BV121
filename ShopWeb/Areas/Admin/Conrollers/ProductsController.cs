using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Constants;
using ShopWeb.Data;
using ShopWeb.Data.Entities;
using ShopWeb.Models.Helpers;
using ShopWeb.Areas.Admin.Models.Products;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Helpers;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ShopWeb.Interfaces;

namespace ShopWeb.Areas.Admin.Conrollers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class ProductsController : Controller
    {
        private readonly AppEFContext _appContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public ProductsController(AppEFContext appContext, IMapper mapper, 
            IConfiguration configuration, IProductService productService)
        {
            _appContext = appContext;
            _mapper = mapper;
            _configuration = configuration;
            _productService = productService;
        }

        public async Task<IActionResult> Index(ProductSearchViewModel search)
        {
            //var name = User.Identity.Name;

            var model = await _productService.SerchData(search);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Categories = await _productService.GetSelectCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            bool isValide = true;
            if (!ModelState.IsValid)
                isValide = false;
            decimal price;
            if (!decimal.TryParse(model.Price, out price))
            {
                this.ModelState.AddModelError("Price", "Введіть число");
                isValide = false;
            }
            else if (price < (decimal)0.01)
            {
                this.ModelState.AddModelError("Price", "Введіть число більше нуля");
                isValide = false;
            }

            if (!isValide)
            {
                if (model.Categories == null)
                {
                    model.Categories = await _productService.GetSelectCategoriesAsync();
                }
                return View(model);
            }
            var prod = new ProductEntity
            {
                DateCreated = DateTime.Now,
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = price
            };
            _appContext.Products.Add(prod);
            _appContext.SaveChanges();

            foreach (var img in model.Images)
            {
                var item = _appContext.ProductImages.SingleOrDefault(x => x.Id == img);
                item.ProductId = prod.Id;
                _appContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _appContext.Products
                .Include(x => x.ProductImages)
                .AsQueryable()
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<ProductEditViewModel>(x))
                .SingleOrDefault();

            model.Categories = _appContext.Categories
                                .Select(x => _mapper.Map<SelectItemViewModel>(x))
                                .ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            bool isValide = true;
            if (!ModelState.IsValid)
                isValide = false;
            decimal price;
            if (!decimal.TryParse(model.Price, out price))
            {
                this.ModelState.AddModelError("Price", "Введіть число");
                isValide = false;
            }
            else if (price < (decimal)0.01)
            {
                this.ModelState.AddModelError("Price", "Введіть число більше нуля");
                isValide = false;
            }

            if (!isValide)
            {
                if (model.Categories == null)
                {
                    model.Categories = _appContext.Categories
                    .Select(x => _mapper.Map<SelectItemViewModel>(x))
                    .ToList();
                }
                return View(model);
            }
            if (model.RemoveImages != null)
            {
                foreach (var img in model.RemoveImages)
                {
                    var del = _appContext.ProductImages
                        .SingleOrDefault(x => x.Name == img);
                    _appContext.Remove(del);
                    _appContext.SaveChanges();
                    string dirSaveImage = Path
                        .Combine(Directory.GetCurrentDirectory(), "images", img);
                    if (System.IO.File.Exists(dirSaveImage))
                        System.IO.File.Delete(dirSaveImage);
                }
            }

            var editProduct = _appContext.Products
                .SingleOrDefault(x => x.Id == model.Id);

            editProduct.Name = model.Name;
            editProduct.CategoryId = model.CategoryId;
            editProduct.Description = model.Description;
            editProduct.Price = price;
            _appContext.SaveChanges();

            if (model.Images != null)
            {
                foreach (var img in model.Images)
                {
                    var item = _appContext.ProductImages.SingleOrDefault(x => x.Id == img);
                    item.ProductId = model.Id;
                    _appContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ProductImageItemViewModel> Upload(ProductImageCreateViewModel model)
        {
            string fileName = string.Empty;
            if (model.File != null)
            {
                //string exp = Path.GetExtension(model.File.FileName);
                var bmp = ImageWorker.IFormFileToBitmap(model.File);
                
                fileName = Path.GetRandomFileName() + ".jpg";
                string[] imageSizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                foreach(var imageSize in imageSizes)
                {
                    int size = int.Parse(imageSize);
                    string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), "images", $"{size}_{fileName}");

                    var saveImage = ImageWorker.CompressImage(bmp, size, size, true, false);
                    saveImage.Save(dirSaveImage, ImageFormat.Jpeg);
                }

                
                //using (var stream = System.IO.File.Create(dirSaveImage))
                //{
                //    await model.File.CopyToAsync(stream);
                //}
            }
            ProductImageEntity image = new ProductImageEntity();
            image.Name = fileName;
            image.DateCreated = DateTime.UtcNow;
            _appContext.ProductImages.Add(image);
            _appContext.SaveChanges();
            return new ProductImageItemViewModel { Id = image.Id, Name = "300_"+image.Name };
        }
    }
}
