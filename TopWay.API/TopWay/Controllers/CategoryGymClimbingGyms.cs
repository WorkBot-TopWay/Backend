using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/category-gyms/{climbingGymId}/climbingGyms")]
[Produces("application/json")]
public class CategoryGymClimbingGyms: ControllerBase
{
    private readonly ICategoryGymService _categoryGymService;
    private readonly IMapper _mapper;

    public CategoryGymClimbingGyms(ICategoryGymService categoryGymService, IMapper mapper)
    {
        _categoryGymService = categoryGymService;
        _mapper = mapper;
    } 
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all climbing gyms or find by climbing gym id",
        Description = "Get all climbing gyms or find by climbing gym id",
        OperationId = "GetClimbingGyms",
        Tags = new[] { "CategoryGyms" })]
    public async Task<IEnumerable<CategoriesResource>> GetCategoriesByGymId(int climbingGymId)
    {
        var categories = await _categoryGymService.FindCategoriesByGymIdAsync(climbingGymId);
        var resources = _mapper.Map<IEnumerable<Categories>, IEnumerable<CategoriesResource>>(categories);
        return resources;
    }
}