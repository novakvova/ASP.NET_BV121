using ShopWeb.Models.Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShopWeb.Areas.Admin.Models.Products
{
    public class ProductSearchViewModel
    {
        [DisplayName("Назва")]
        public string Name { get; set; }

        [DisplayName("Категорія")]
        public string CategoryId { get; set; }

        [DisplayName("Відоразити")]
        public int PageSize { get; set; } = 10;

        public int? Page { get; set; }

        public List<SelectItemViewModel> Categories { get; set; }
    }
}
