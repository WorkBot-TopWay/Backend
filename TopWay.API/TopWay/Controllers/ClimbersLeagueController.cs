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
public class ClimbersLeagueController : ControllerBase
{
    private readonly IClimbersLeagueService _climbersLeagueService;
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;

    public ClimbersLeagueController(IClimbersLeagueService climbersLeagueService, ILeagueService leagueService,
        IMapper mapper)
    {
        _climbersLeagueService = climbersLeagueService;
        _leagueService = leagueService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ClimbersLeagueResource>> GetAllAsync()
    {
        var climbersLeague = await _climbersLeagueService.GetAll();
        var resources = _mapper.Map<IEnumerable<ClimbersLeague>, IEnumerable<ClimbersLeagueResource>>(climbersLeague);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var climbersLeague = await _climbersLeagueService.FindByIdAsync(id);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<ClimbersLeague, ClimbersLeagueResource>(climbersLeague);
        return Ok(resource);
    }
    
    [HttpGet("FindScalersByLeagueAndClimbingGymId")]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId)
    {
        var climbersLeague = await _climbersLeagueService.FindScalersByLeagueAndClimbingGymId(leagueId, climbingGymId);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(climbersLeague);
        return Ok(resource);
    }
    
    [HttpGet("FindByClimbingGymIdAndScalerIdAndLeagueId")]
    public async Task<ActionResult<IEnumerable<ClimbersLeague>>> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId)
    {
        var climbersLeague = await _climbersLeagueService.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
        if (climbersLeague == null)
            return NotFound();
        var resource = _mapper.Map<ClimbersLeague, ClimbersLeagueResource>(climbersLeague);
        return Ok(resource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveClimbersLeagueResource resource,int climbingGymId, int scalerId, int leagueId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var climbersLeague = _mapper.Map<SaveClimbersLeagueResource, ClimbersLeague>(resource);
        var result = await _climbersLeagueService.AddAsync(climbersLeague, climbingGymId, scalerId, leagueId);
        var resultAdd = await _leagueService.UpdateNumberParticipant(leagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var climbersLeagueResource = _mapper.Map<ClimbersLeague, ClimbersLeagueResource>(result.Resource);
        return Ok(climbersLeagueResource);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId, int leagueId)
    {
        var climbersLeague = await _climbersLeagueService.FindByClimbingGymIdAndScalerIdAndLeagueId(climbingGymId, scalerId, leagueId);
        if (climbersLeague == null)
            return NotFound();
        var result = await _climbersLeagueService.Delete(climbingGymId, scalerId, leagueId);
        var deleter = await _leagueService.DeleteParticipant(leagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var climbersLeagueResource = _mapper.Map<ClimbersLeague, ClimbersLeagueResource>(result.Resource);
        return Ok(climbersLeagueResource);
    }
}