using System.ComponentModel;

namespace ShopWeb.Areas.Admin.Models.Products
{
    public class ProductSearchViewModel
    {
        [DisplayName("Назва")]
        public string Name { get; set; }
        [DisplayName("Категорія")]
        public string CategoryId { get; set; }
    }
}
