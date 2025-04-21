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

                if(product is null)
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

                if(product is null)
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
                //await _unitOfWork.GetGenericRepository<Product>().AddAsync(addProductDTO);
                await _unitOfWork.ProductRepository.AddAsync(addProductDTO);


                //if(product is null)
                //{
                //    return BadRequest(new ResponseAPI(400));
                //}
                return Ok();
                

            }
            catch (Exception ex)
            {   

                return BadRequest(new ResponseAPI(400));
            }
        }
    }
}
