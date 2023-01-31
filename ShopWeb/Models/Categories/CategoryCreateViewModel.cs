using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models.Categories
{
    /// <summary>
    /// Модель для створення нової категорії
    /// </summary>
    public class CategoryCreateViewModel
    {
        [Display(Name="Назва категорії")]
        [Required(ErrorMessage ="Вкажіть назву")]
        public string Name { get; set; }
        [Display(Name = "Фото категорії")]
        public IFormFile UploadImage { get; set; }

    }
}
