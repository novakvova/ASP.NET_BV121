using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Data;
using ShopWeb.Models.Helpers;
using ShopWeb.Models.Products;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ShopWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppEFContext _appContext;
        private readonly IMapper _mapper;

        public ProductsController(AppEFContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //var model = _appContext.Products.Select(x=> new ProductItemViewModel
            //{
            //    Id= x.Id,
            //    Name = x.Name,
            //    Price= x.Price,
            //    CategoryName=x.Category.Name
            //}).ToList();
            var model = _appContext.Products
                .AsQueryable()
                .Include(x=>x.Category)
                .Select(x => _mapper.Map<ProductItemViewModel>(x))
                .ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Categories = _appContext.Categories
                .Select(x=>_mapper.Map<SelectItemViewModel>(x))
                .ToList();

            return View(model);
        }
    }
}
