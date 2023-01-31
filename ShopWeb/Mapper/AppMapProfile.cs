using AutoMapper;
using ShopWeb.Data.Entities;
using ShopWeb.Models.Products;

namespace ShopWeb.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile() 
        {
            CreateMap<ProductEntity, ProductItemViewModel>()
                .ForMember(p=>p.CategoryName, 
                    opt=> opt.MapFrom(entity=> entity.Category.Name));
        }
    }
}
