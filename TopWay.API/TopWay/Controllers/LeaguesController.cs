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
[SwaggerTag(" Create, Read, Update and Delete a Leagues")]
public class LeaguesController: ControllerBase
{
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;

    public LeaguesController(ILeagueService leagueService, IMapper mapper)
    {
        _leagueService = leagueService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all leagues or a specific league",
        Description = "Get all leagues or a specific league",
        OperationId = "GetLeagues",
        Tags = new[] { "Leagues" })]
    public async  Task<IActionResult> GetAllAsync([FromQuery] string ? climbingGymId=null)
    {

        if (climbingGymId != null)
        {
            int id = int.Parse(climbingGymId);
            var league = await _leagueService.FindByClimbingGymId(id);
            var resource = _mapper.Map<IEnumerable<League>, IEnumerable<LeagueResource>>(league);
            return Ok(resource);
        }

        var leagues = await _leagueService.GetAll();
        var resources = _mapper.Map<IEnumerable<League>, IEnumerable<LeagueResource>>(leagues);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a specific league",
        Description = "Get existing league",
        OperationId = "GetLeague",
        Tags = new[] { "Leagues" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var league = await _leagueService.GetById(id);
        if (league == null)
            return NotFound();
        var resource = _mapper.Map<League, LeagueResource>(league);
        return Ok(resource);
    }
    
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a new league",
        Description = "Create a new league",
        OperationId = "CreateLeague",
        Tags = new[] { "Leagues" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveLeagueResource resource, int climbingGymId, int scaleId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var league = _mapper.Map<SaveLeagueResource, League>(resource);
        var result = await _leagueService.Add(league, climbingGymId, scaleId);
        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResource = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update an existing league",
        Description = "Update an existing league",
        OperationId = "UpdateLeague",
        Tags = new[] { "Leagues" })]
    public async Task<IActionResult> PutAsync([FromBody] SaveLeagueResource league, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var leagueResource = _mapper.Map<SaveLeagueResource, League>(league);
        var result = await _leagueService.Update(leagueResource, id);

        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResourceUpdated = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResourceUpdated);
    }
  
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete an existing league",
        Description = "Delete an existing league",
        OperationId = "DeleteLeague",
        Tags = new[] { "Leagues" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _leagueService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResource = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResource);
    }
    
}