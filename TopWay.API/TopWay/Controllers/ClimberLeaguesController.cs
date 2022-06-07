using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/climber-leagues")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Delete Climber Leagues")]
public class ClimberLeaguesController : ControllerBase
{
    private readonly IClimbersLeagueService _climbersLeagueService;
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;

    public ClimberLeaguesController(IClimbersLeagueService climbersLeagueService, ILeagueService leagueService,
        IMapper mapper)
    {
        _climbersLeagueService = climbersLeagueService;
        _leagueService = leagueService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Climber Leagues or find one by climber id and scaler id and league id",
        Description = "Get all Climber Leagues or find one by climber id and scaler id and league id",
        OperationId = "GetClimberLeagues",
        Tags = new[] { "ClimberLeagues" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? climbinggymId=null, [FromQuery] string? scalerId=null,[FromQuery] string? leagueId=null)
    {
        if (climbinggymId != null && scalerId != null && leagueId != null)
        {
            int climbinGgmIdInt = Int32.Parse(climbinggymId);
            int scalerIdInt = Int32.Parse(scalerId);
            int leagueIdInt = Int32.Parse(leagueId);
            var climbersLeagueGet = await _climbersLeagueService.FindByClimbingGymIdAndScalerIdAndLeagueId(climbinGgmIdInt, scalerIdInt, leagueIdInt);
            if (climbersLeagueGet == null)
                return NotFound();
            var resource = _mapper.Map<ClimberLeagues, ClimbersLeagueResource>(climbersLeagueGet);
            return Ok(resource);
        }

        var climbersLeague = await _climbersLeagueService.GetAll();
        var resources = _mapper.Map<IEnumerable<ClimberLeagues>, IEnumerable<ClimbersLeagueResource>>(climbersLeague);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a Climber League by id",
        Description = "Get a existing Climber League by id",
        OperationId = "GetClimberLeague",
        Tags = new[] { "ClimberLeagues" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var climbersLeague = await _climbersLeagueService.FindByIdAsync(id);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<ClimberLeagues, ClimbersLeagueResource>(climbersLeague);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a Climber League",
        Description = "Create a new Climber League",
        OperationId = "CreateClimberLeague",
        Tags = new[] { "ClimberLeagues" })]
    
    public async Task<IActionResult> PostAsync([FromBody] SaveClimbersLeagueResource resource,int climbingGymId, int scalerId, int leagueId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var climbersLeague = _mapper.Map<SaveClimbersLeagueResource, ClimberLeagues>(resource);
        var result = await _climbersLeagueService.AddAsync(climbersLeague, climbingGymId, scalerId, leagueId);
        var resultAdd = await _leagueService.UpdateNumberParticipant(leagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var climbersLeagueResource = _mapper.Map<ClimberLeagues, ClimbersLeagueResource>(result.Resource);
        return Ok(climbersLeagueResource);
    }
    
    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete a Climber League by id",
        Description = "Delete a existing Climber League by id",
        OperationId = "DeleteClimberLeague",
        Tags = new[] { "ClimberLeagues" })]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId, int leagueId)
    {
        var climbersLeague = await _climbersLeagueService.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
        if (climbersLeague == null)
            return NotFound();
        var result = await _climbersLeagueService.Delete(climbingGymId, scalerId, leagueId);
        var deleter = await _leagueService.DeleteParticipant(leagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var climbersLeagueResource = _mapper.Map<ClimberLeagues, ClimbersLeagueResource>(result.Resource);
        return Ok(climbersLeagueResource);
    }
}