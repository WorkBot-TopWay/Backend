using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/favorites/{scalerId}/climbing-gyms")]
[Produces("application/json")]
public class FavoriteScalersController: ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly IMapper _mapper;
    
    public FavoriteScalersController(IFavoriteService favoriteService, IMapper mapper)
    {
        _favoriteService = favoriteService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Climbing Gyms for a specific Scaler",
        Description = "Get all Climbing Gyms for a specific Scaler",
        OperationId = "GetAllClimbingGymsForAScaler",
        Tags = new[] { "Favorites" })]
    public async Task<IActionResult> FindClimbingGymByScalerIdAsync(int scalerId)
    {
        var climbingGym = await _favoriteService.FindClimbingGymByScalerIdAsync(scalerId);
        var resources = _mapper.Map<IEnumerable<ClimbingGyms>, IEnumerable<ClimbingGymResource>>(climbingGym);
        return Ok(resources);
    }
}