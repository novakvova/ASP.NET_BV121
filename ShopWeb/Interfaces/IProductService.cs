using ShopWeb.Areas.Admin.Models.Products;
using ShopWeb.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWeb.Interfaces
{
    public interface IProductService
    {
        Task<List<SelectItemViewModel>> GetSelectCategoriesAsync(bool isDefault=false);

        Task<ProductListViewModel> SerchData(ProductSearchViewModel search);
    }
}
