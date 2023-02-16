using Microsoft.AspNetCore.Http;

namespace ShopWeb.Areas.Admin.Models.Products
{
    public class ProductImageCreateViewModel
    {
        public IFormFile File { get; set; }
    }
}
