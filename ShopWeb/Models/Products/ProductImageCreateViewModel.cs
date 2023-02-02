using Microsoft.AspNetCore.Http;

namespace ShopWeb.Models.Products
{
    public class ProductImageCreateViewModel
    {
        public IFormFile File { get; set; }
    }
}
