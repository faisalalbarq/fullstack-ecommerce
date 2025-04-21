using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Service.ImageService;


namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(StoreContext storeContext, IMapper mapper, IImageManagementService imageManagementService) : base(storeContext)
        {
            _storeContext = storeContext;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO addProductDTO)
        {
            if(addProductDTO is null)
            {
                return false;
            }

            var product = _mapper.Map<Product>(addProductDTO);

            await _storeContext.Products.AddAsync(product);
            await _storeContext.SaveChangesAsync();

            var ImagePath = await _imageManagementService
                .AddImageAsync(addProductDTO.Pictures, addProductDTO.Name);


            var picture = ImagePath.Select(path => new Picture
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();

            await _storeContext.pictures.AddRangeAsync(picture);
            await _storeContext.SaveChangesAsync();
            return true;
        }
    }
}
