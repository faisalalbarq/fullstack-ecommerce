using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;
using Ecommerce.Core.Interfaces.Repositories.Contract;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Service.ImageService;
using Microsoft.EntityFrameworkCore;


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
            if (addProductDTO is null)
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



        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }

            //var product = await _storeContext.Products.FindAsync(updateProductDTO.Id);

            var product = await _storeContext.Products
                .Include(product => product.Category)
                .Include(product => product.Pictures)
                .FirstOrDefaultAsync(product => product.Id == updateProductDTO.Id);

            if (product is null)
            {
                return false;
            }

            _mapper.Map(updateProductDTO, product);

            var picture = await _storeContext.pictures
                .Where(p => p.ProductId == updateProductDTO.Id)
                .ToListAsync();

            foreach (var item in picture)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _storeContext.pictures.RemoveRange(picture);


            var ImagePath = await _imageManagementService
                .AddImageAsync(updateProductDTO.Pictures, updateProductDTO.Name);


            var pictures = ImagePath.Select(path => new Picture
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            
            await _storeContext.pictures.AddRangeAsync(pictures);
            await _storeContext.SaveChangesAsync();

            return true;


        }


        public async Task DeleteAsync(Product product)
        {
            // i want to make the parameter as an entity, not id, because i want to delete the product and its pictures
            var pictures = await _storeContext.pictures.Where(p => p.ProductId == product.Id).ToListAsync();

            foreach (var item in pictures)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _storeContext.Products.Remove(product);
            // when i delete the product in database, the pictures will be removed automaticly
            await _storeContext.SaveChangesAsync();
        }
    }
}
