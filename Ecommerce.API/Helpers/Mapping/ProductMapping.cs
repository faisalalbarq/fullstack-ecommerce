using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;

namespace Ecommerce.API.Helpers.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                opt => 
                opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Picture, PictureDTO>().ReverseMap();

            CreateMap<AddProductDTO, Product>()
                .ForMember(x => x.Pictures, op => op.Ignore())
                .ReverseMap();
        }
    }
}
