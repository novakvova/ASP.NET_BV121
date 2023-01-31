using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models.Products
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        [Display(Name="Назва товару")]
        public string Name { get; set; }
        [Display(Name="Ціна")]
        public decimal Price { get; set; }
        [Display(Name="Категорії")]
        public string CategoryName { get; set; }
    }
}
