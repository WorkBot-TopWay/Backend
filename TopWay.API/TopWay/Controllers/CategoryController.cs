using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet ("{name?}")]
    
    public async Task<IActionResult> GetAllAsync(string name ="")
    {
        if (name != null)
        {
            var result = await _categoryService.FindByNameAsync(name);
            if(result == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<Category, CategoryResource>(result);
            return Ok(resource);
        }

        var categories = await _categoryService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _categoryService.FindByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Category, CategoryResource>(result);
        return Ok(resource);
    }
    
    /*[HttpGet("{name}")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var result = await _categoryService.FindByNameAsync(name);
        if(result == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Category, CategoryResource>(result);
        return Ok(resource);
    }*/
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveCategoryResource, Category>(resource);
        var result = await _categoryService.SaveAsync(category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
        return Ok(categoryResource);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveCategoryResource, Category>(resource);
        var result = await _categoryService.UpdateAsync(id, category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
        return Ok(categoryResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _categoryService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
        return Ok(categoryResource);
    }

}