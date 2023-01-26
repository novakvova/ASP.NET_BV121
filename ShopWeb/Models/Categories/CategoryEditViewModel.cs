using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models.Categories
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }
        [Display(Name="Назва")]
        public string Name { get; set; }
    }
}
