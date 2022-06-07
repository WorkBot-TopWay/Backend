using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete Features")]
public class FeaturesController: ControllerBase
{
    private readonly IFeatureService _favoriteService;
    private readonly IMapper _mapper;

    public FeaturesController(IFeatureService favoriteService, IMapper mapper)
    {
        _favoriteService = favoriteService;
        _mapper = mapper;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all features",
        Description = "Get all features",
        OperationId = "GetAllFeatures",
        Tags = new[] { "Features" })]
    public async  Task<IActionResult> GetByIdAsync(int id)
    {
        var feature = await _favoriteService.FindByIdAsync(id);
        if (feature == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Features, FeaturesResource>(feature);
        return Ok(resource);
    }
    [HttpPost("{climbingGymId}")]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a feature",
        Description = "Create a new feature",
        OperationId = "CreateFeature",
        Tags = new[] { "Features" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveFeaturesResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        var feature = _mapper.Map<SaveFeaturesResource, Features>(resource);
        var result = await _favoriteService.AddAsync(feature, climbingGymId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var featureResource = _mapper.Map<Features, FeaturesResource>(result.Resource);
        return Ok(featureResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a feature",
        Description = "Update existing feature",
        OperationId = "UpdateFeature",
        Tags = new[] { "Features" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFeaturesResource resource)
    {
        var feature = _mapper.Map<SaveFeaturesResource, Features>(resource);
        var result = await _favoriteService.Update(feature, id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var featureResource = _mapper.Map<Features, FeaturesResource>(result.Resource);
        return Ok(featureResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a feature",
        Description = "Delete existing feature",
        OperationId = "DeleteFeature",
        Tags = new[] { "Features" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _favoriteService.Delete(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var featureResource = _mapper.Map<Features, FeaturesResource>(result.Resource);
        return Ok(featureResource);
    }
}