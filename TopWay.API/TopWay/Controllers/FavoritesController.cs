using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/favorites")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete favorites")]
public class FavoritesController: ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly IMapper _mapper;
    
    public FavoritesController(IFavoriteService favoriteService, IMapper mapper)
    {
        _favoriteService = favoriteService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all favorites or find by climbing gym id and scaler id",
        Description = "Get all favorites or find by climbing gym id and scaler id",
        OperationId = "GetFavorites",
        Tags = new[] { "Favorites" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string ? climbingGymId=null, [FromQuery] string ? scalerId=null)
    {
        if(climbingGymId != null && scalerId != null)
        {
            int ClimbingGymId = int.Parse(climbingGymId);
            int ScalerId = int.Parse(scalerId);
            var favorites = await _favoriteService.FindByClimbingGymIdAndScalerIdAsync(ClimbingGymId, ScalerId);
            if (favorites == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<Favorite, FavoriteResource>(favorites);
            return Ok(resource);
        }
        var favorite = await _favoriteService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<FavoriteResource>>(favorite);
        return Ok(resources);
    }
    
    [HttpGet("findByScalerId/{id}")]
    [SwaggerOperation(
        Summary = "Find by scaler id",
        Description = "Find existing favorite by scaler id",
        OperationId = "FindByScalerId",
        Tags = new[] { "Favorites" })]
    public async Task<IEnumerable<FavoriteResource>> FindByScalerIdAsync(int id)
    {
        var favorite = await _favoriteService.FindByScalerIdAsync(id);
        var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<FavoriteResource>>(favorite);
        return resources;
    }
    

    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a new favorite",
        Description = "Create a new favorite",
        OperationId = "CreateFavorite",
        Tags = new[] { "Favorites" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveFavoriteResource resource, int climbingGymId,
        int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var favorite = _mapper.Map<SaveFavoriteResource, Favorite>(resource);
        var result = await _favoriteService.AddAsync(favorite, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update an existing favorite",
        Description = "Update an existing favorite",
        OperationId = "UpdateFavorite",
        Tags = new[] { "Favorites" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFavoriteResource resource, int climbingGymId,
        int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var favorite = _mapper.Map<SaveFavoriteResource, Favorite>(resource);
        var result = await _favoriteService.UpdateAsync(favorite, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete an existing favorite",
        Description = "Delete an existing favorite",
        OperationId = "DeleteFavorite",
        Tags = new[] { "Favorites" })]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId)
    {
        var result = await _favoriteService.Delete(climbingGymId, scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var favoriteResource = _mapper.Map<Favorite, FavoriteResource>(result.Resource);
        return Ok(favoriteResource);
    }
}