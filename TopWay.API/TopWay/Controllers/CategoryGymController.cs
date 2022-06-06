using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CategoryGymController: ControllerBase
{
    private readonly ICategoryGymService _categoryGymService;
    private readonly IMapper _mapper;

    public CategoryGymController(ICategoryGymService categoryGymService, IMapper mapper)
    {
        _categoryGymService = categoryGymService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryGymResource>> GetAllAsync()
    {
        var categoryGyms = await _categoryGymService.GetAll();
        var resources = _mapper.Map<IEnumerable<CategoryGym>, IEnumerable<CategoryGymResource>>(categoryGyms);
        return resources;
    }
    
    [HttpGet("findClimbingGymsByCategoryId/{categoryId}")]
    public async Task<IEnumerable<ClimbingGymResource>> GetClimbingGymsByCategoryId(int categoryId)
    {
        var climbingGyms = await _categoryGymService.FindClimbingGymsByCategoryIdAsync(categoryId);
        var resources = _mapper.Map<IEnumerable<ClimbingGym>, IEnumerable<ClimbingGymResource>>(climbingGyms);
        return resources;
    }
    
    [HttpGet("findCategoriesByGymId/{ClimbingGymId}")]
    public async Task<IEnumerable<CategoryResource>> GetCategoriesByGymId(int ClimbingGymId)
    {
        var categories = await _categoryGymService.FindCategoriesByGymIdAsync(ClimbingGymId);
        var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var categoryGym = await _categoryGymService.FindByIdAsync(id);

        if (categoryGym == null)
            return NotFound();

        var resource = _mapper.Map<CategoryGym, CategoryGymResource>(categoryGym);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCategoryGymResource resource, int climbingGymId, int categoryId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var categoryGym = _mapper.Map<SaveCategoryGymResource, CategoryGym>(resource);
        var result = await _categoryGymService.SaveAsync(categoryGym, climbingGymId, categoryId);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryGymResource = _mapper.Map<CategoryGym, CategoryGymResource>(result.Resource);
        return Ok(categoryGymResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int categoryId)
    {
        var result = await _categoryGymService.DeleteAsync(climbingGymId, categoryId);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryGymResource = _mapper.Map<CategoryGym, CategoryGymResource>(result.Resource);
        return Ok(categoryGymResource);
    }
    
}