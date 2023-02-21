using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopWeb.Areas.Admin.Models.Products;
using ShopWeb.Data;
using ShopWeb.Interfaces;
using ShopWeb.Models.Helpers;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly AppEFContext _appContext;
        private readonly IMapper _mapper;
        public ProductService(AppEFContext appContext, IMapper mapper)
        {
            _appContext= appContext;
            _mapper= mapper;
        }
        public async Task<List<SelectItemViewModel>> GetSelectCategoriesAsync(bool isDefault = false)
        {
            var model = await _appContext.Categories
                .Select(x => _mapper.Map<SelectItemViewModel>(x))
                .ToListAsync();
            return model;
        }

        public async Task<ProductListViewModel> SerchData(ProductSearchViewModel search)
        {
            ProductListViewModel model = new ProductListViewModel();
            var query = _appContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }

            model.Search = search;
            model.Search.Categories = await GetSelectCategoriesAsync();

            model.Count = query.Count();

            PaginationViewModel pagination = new PaginationViewModel();
            pagination.CurrentPage = 1;
            pagination.PageSize = search.PageSize;
            pagination.TotalItems = query.Count();

            model.Pagination = pagination;
   
            query = query
                .OrderBy(x => x.Name)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

            model.Products = await query
                .Include(x => x.Category)
                .Select(x => _mapper.Map<ProductItemViewModel>(x))
                .ToListAsync();

            return model;
        }
    }
}
