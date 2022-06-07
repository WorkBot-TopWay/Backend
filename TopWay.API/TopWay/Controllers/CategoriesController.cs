using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete Categories")]
public class CategoriesController: ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all categories or by their names",
        Description = "Get all existing categories or filter them by name",
        OperationId = "GetCategories",
        Tags = new[] { "Categories" }
        )]
    public async Task<IActionResult> GetAllAsync([FromQuery]string? name=null)
    {
        if (name != null)
        {
            var result = await _categoryService.FindByNameAsync(name);
            if(result == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<Categories, CategoriesResource>(result);
            return Ok(resource);
        }

        var categories = await _categoryService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Categories>, IEnumerable<CategoriesResource>>(categories);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a category by its id",
        Description = "Get a existing category by its id",
        OperationId = "GetCategoriesById",
        Tags = new[] { "Categories" }
        )]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _categoryService.FindByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Categories, CategoriesResource>(result);
        return Ok(resource);
    }
    
    
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new category",
        Description = "Create a new category",
        OperationId = "CreateCategories",
        Tags = new[] { "Categories" }
        )]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The category is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Category Information to Add", Required = true)] SaveCategoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveCategoryResource, Categories>(resource);
        var result = await _categoryService.SaveAsync(category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Categories, CategoriesResource>(result.Resource);
        return Ok(categoryResource);
    }
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a category",
        Description = "Update a existing category",
        OperationId = "UpdateCategories",
        Tags = new[] { "Categories" }
        )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveCategoryResource, Categories>(resource);
        var result = await _categoryService.UpdateAsync(id, category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Categories, CategoriesResource>(result.Resource);
        return Ok(categoryResource);
    }
    
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a category",
        Description = "Delete a existing category",
        OperationId = "DeleteCategories",
        Tags = new[] { "Categories" }
        )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _categoryService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Categories, CategoriesResource>(result.Resource);
        return Ok(categoryResource);
    }

}