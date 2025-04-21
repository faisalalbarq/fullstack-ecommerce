using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;

namespace Ecommerce.API.Helpers.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping() {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
