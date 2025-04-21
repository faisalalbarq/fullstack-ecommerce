using AutoMapper;
using Ecommerce.API.Helpers.Errors;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities.Product;
using Ecommerce.Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _unitOfWork.GetGenericRepository<Category>().GetAllAsync();
                if (categories is null)
                {
                    return BadRequest();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
                throw;
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.GetGenericRepository<Category>().GetByIdAsync(id);
                if (category is null)
                {
                    return BadRequest();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                /// var category = new Category()
                /// {
                ///     Name = categoryDTO.name,
                ///     Description = categoryDTO.description
                /// };
                var category = _mapper.Map<Category>(categoryDTO);

                await _unitOfWork.GetGenericRepository<Category>().AddAsync(category);
                return Ok(new ResponseAPI(200, "Category added successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            try
            {
                /// var category = new Category()
                /// {
                ///     Name = updateCategoryDTO.name,
                ///     Description = updateCategoryDTO.description,
                ///     Id = updateCategoryDTO.id
                /// };
                
                var category = _mapper.Map<Category>(updateCategoryDTO);

                await _unitOfWork.GetGenericRepository<Category>().UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Category Updated Successfully!"));
            }
            catch (Exception ex)    
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.GetGenericRepository<Category>().DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Category Deleted Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
