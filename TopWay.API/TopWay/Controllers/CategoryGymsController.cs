using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/category-gyms")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Delete CategoryGyms")]
public class CategoryGymsController: ControllerBase
{
    private readonly ICategoryGymService _categoryGymService;
    private readonly IMapper _mapper;

    public CategoryGymsController(ICategoryGymService categoryGymService, IMapper mapper)
    {
        _categoryGymService = categoryGymService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all CategoryGyms",
        Description = "Get all existing CategoryGyms",
        OperationId = "GetAllCategoryGyms",
        Tags = new[] { "CategoryGyms" })]
    public async Task<IEnumerable<CategoryGymResource>> GetAllAsync()
    {
        var categoryGyms = await _categoryGymService.GetAll();
        var resources = _mapper.Map<IEnumerable<CategoryGyms>, IEnumerable<CategoryGymResource>>(categoryGyms);
        return resources;
    }
    

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get CategoryGym by Id",
        Description = "Get CategoryGym by Id",
        OperationId = "GetCategoryGymById",
        Tags = new[] { "CategoryGyms" })]
    public async Task<IActionResult> GetAsync(int id)
    {
        var categoryGym = await _categoryGymService.FindByIdAsync(id);

        if (categoryGym == null)
            return NotFound();

        var resource = _mapper.Map<CategoryGyms, CategoryGymResource>(categoryGym);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The category or climbing gym is invalid")]
    [SwaggerOperation(
        Summary = "Create CategoryGym",
        Description = "Create new CategoryGym",
        OperationId = "CreateCategoryGym",
        Tags = new[] { "CategoryGyms" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveCategoryGymResource resource, int climbingGymId, int categoryId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var categoryGym = _mapper.Map<SaveCategoryGymResource, CategoryGyms>(resource);
        var result = await _categoryGymService.SaveAsync(categoryGym, climbingGymId, categoryId);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryGymResource = _mapper.Map<CategoryGyms, CategoryGymResource>(result.Resource);
        return Ok(categoryGymResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete CategoryGym",
        Description = "Delete existing CategoryGym",
        OperationId = "DeleteCategoryGym",
        Tags = new[] { "CategoryGyms" })]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int categoryId)
    {
        var result = await _categoryGymService.DeleteAsync(climbingGymId, categoryId);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryGymResource = _mapper.Map<CategoryGyms, CategoryGymResource>(result.Resource);
        return Ok(categoryGymResource);
    }
    
}