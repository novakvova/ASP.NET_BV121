using ShopWeb.Models.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace ShopWeb.Models.Products
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва товару")]
        [Required(ErrorMessage = "Вкажіть назву")]
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
        public int[] Images { get; set; }
        public string[] OldImages { get; set; }
    }
}
