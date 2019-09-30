using AutoMapper;
using CashinGame.Quiz.Api.Models;
using CashinGame.Quiz.Entity.Interface;
using CashinGame.Quiz.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashinGame.Quiz.Api.Controllers
{
    [Route("api/categories")]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoriesController(ICategoryRepository repository, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of category
        /// </summary>
        /// <returns>An ActionResult of type IEnumerable of Category</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categoriesFromRepo = await _repository.GetAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categoriesFromRepo));
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="categoryId">The id of the category you want to get</param>
        /// <returns></returns>
        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            var categoryFromRepo = await _repository.GetByIdAsync(categoryId);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(categoryFromRepo));
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>An ActionResult of type category</returns>
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryDto category)
        {
            if (category == null) return BadRequest();

            if (await _repository.isExists(category.Name))
                return BadRequest("The category name inputed already exist");

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var categoryToAdd = _mapper.Map<Category>(category);
            _repository.Add(categoryToAdd);

            if (!await _repository.SaveChangesAsync())
                throw new Exception("Creating  category failed on save.");

            return CreatedAtRoute("GetCategory", new { categoryId = categoryToAdd.Id },
                _mapper.Map<Category>(categoryToAdd));
        }

        /// <summary>
        /// Update an category
        /// </summary>
        /// <param name="category">The category with updated values</param>
        /// <returns>An ActionResult of type Category</returns>
        /// <response code="422">Validation error</response>
        [HttpPut("{categoryId}", Name = "UpdateCategory")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<Category>> UpdateCategory(Guid categoryId, UpdateCategoryDto category)
        {
            if (category == null) return BadRequest();

            if (!ModelState.IsValid) return new UnprocessableEntityObjectResult(ModelState);

            var categoryFromRepo =  await _repository.GetByIdAsync(categoryId);
            if (categoryFromRepo == null) return NotFound();

            _mapper.Map(category, categoryFromRepo);
            _repository.Update(categoryFromRepo);

            if (!await _repository.SaveChangesAsync())
                throw new Exception("An error occured while trying to updating categories");

            return Ok(_mapper.Map<Category>(categoryFromRepo));
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>An ActionResult of type Category</returns>
        [HttpDelete("{categoryId}", Name = "DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            var categoryFromRepo = await _repository.GetByIdAsync(categoryId);
            if (categoryFromRepo == null) return NotFound();

             _repository.Delete(categoryFromRepo);

            if (!await _repository.SaveChangesAsync())
                throw new Exception($"Deleting category {categoryId} failed to delete.");

            return NoContent();
        }


    }
}