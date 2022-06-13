using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
[ApiController]
[Route("/api/v1/climber-leagues/climbing-gym/{climbingGymId}/scaler/{scalerId}/leagues")]
[Produces("application/json")]
public class ClimberLeagueLeaguesController:ControllerBase
{
    private readonly IClimbersLeagueService _climbersLeagueService;
    private readonly IMapper _mapper;

    public ClimberLeagueLeaguesController(IClimbersLeagueService climbersLeagueService, IMapper mapper)
    {
        _climbersLeagueService = climbersLeagueService;
        _mapper = mapper;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all League Climbers",
        Description = "Get all League Climbers",
        OperationId = "GetAllClimbersLeague",
        Tags = new[] { "ClimberLeagues" })]
    public async Task<ActionResult<IEnumerable<League>>> FindLeaguesByClimbingGymIdAndScalerId(int climbingGymId,int scalerId)
    {
        var climbersLeague = await _climbersLeagueService.FindLeaguesByClimbingGymIdAndScalerId(climbingGymId, scalerId);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<IEnumerable<League>, IEnumerable<LeagueResource>>(climbersLeague);
        return Ok(resource);
    }
}