using AutoMapper;
using Ecommerce.API.Helpers.Errors;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;
using Ecommerce.Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _unitOfWork.GetGenericRepository<Product>()
                    .GetAllAsync(x => x.Category, x => x.Pictures);

                var result = _mapper.Map<List<ProductDTO>>(product);

                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _unitOfWork.GetGenericRepository<Product>().GetByIdAsync(id, x => x.Category, x => x.Pictures);
                var result = _mapper.Map<ProductDTO>(product);

                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            try
            {
                await _unitOfWork.ProductRepository.AddAsync(addProductDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }


        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository
                   .GetByIdAsync(id, x => x.Pictures, x => x.Category);

                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }

                await _unitOfWork.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }
    }
}
