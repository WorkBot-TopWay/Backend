using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
[ApiController]
[Route("/api/v1/climber-leagues/league/{leagueId}/climbing-gym/{climbingGymId}/scalers")]
[Produces("application/json")]
public class ClimberLeagueScalersController: ControllerBase
{
    private readonly IClimbersLeagueService _climbersLeagueService;
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;

    public ClimberLeagueScalersController(IClimbersLeagueService climbersLeagueService, ILeagueService leagueService,
        IMapper mapper)
    {
        _climbersLeagueService = climbersLeagueService;
        _leagueService = leagueService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers in a league",
        Description = "Get all existing scalers in a league",
        OperationId = "GetAllClimbersInLeague",
        Tags = new[] { "ClimberLeagues" })]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbinggymId)
    {
        var climbersLeague = await _climbersLeagueService.FindScalersByLeagueAndClimbingGymId(leagueId, climbinggymId);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(climbersLeague);
        return Ok(resource);
    }
    
}