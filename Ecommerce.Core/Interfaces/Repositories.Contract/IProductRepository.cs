using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;

namespace Ecommerce.Core.Interfaces.Repositories.Contract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDTO addProductDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);

        Task DeleteAsync(Product product);
    }
}
