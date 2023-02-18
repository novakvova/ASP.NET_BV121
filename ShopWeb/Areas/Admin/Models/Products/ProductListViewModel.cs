using System.Collections.Generic;

namespace ShopWeb.Areas.Admin.Models.Products
{
    public class ProductListViewModel
    {
        public List<ProductItemViewModel> Products { get; set; }
        public ProductSearchViewModel Search { get; set; }

        public int Count { get; set; }
    }
}
