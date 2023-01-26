using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models.Categories
{
    /// <summary>
    /// Модель для створення нової категорії
    /// </summary>
    public class CategoryCreateViewModel
    {
        [Display(Name="Назва категорії")]
        public string Name { get; set; }
    }
}
