using ShopWeb.Models.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models.Products
{
    public class ProductCreateViewModel
    {
        [Display(Name="Назва товару")]
        [Required(ErrorMessage ="Вкажіть назву")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Вкажіть ціну")]
        public string Price { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Вкажіть категорію")]
        public int CategoryId { get; set; }
        public List<SelectItemViewModel> Categories { get; set; }
    }
}
