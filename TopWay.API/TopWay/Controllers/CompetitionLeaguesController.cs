using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/competition-leagues")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete Competition Leagues")]
public class CompetitionLeaguesController : ControllerBase
{
    private readonly ICompetitionLeagueService _competitionLeagueService;
    private readonly IMapper _mapper;

    public CompetitionLeaguesController(ICompetitionLeagueService competitionLeagueService, IMapper mapper)
    {
        _competitionLeagueService = competitionLeagueService;
        _mapper = mapper;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all competition leagues or find by league id",
        Description = "Get all competition leagues or find by league id",
        OperationId = "GetCompetitionLeagues",
        Tags = new[] { "CompetitionLeagues" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? leagueId=null)
    {
        if (leagueId != null)
        {
            int LeagueId = int.Parse(leagueId);
            var competitionLeague = await _competitionLeagueService.FindByLeagueIdAsync(LeagueId);
            var resource = _mapper.Map<IEnumerable<CompetitionLeague>, IEnumerable<CompetitionLeagueResource>>(competitionLeague);
            return Ok(resource);
        }

        var competitionLeagues = await _competitionLeagueService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionLeague>, IEnumerable<CompetitionLeagueResource>>(competitionLeagues);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get competition league by id",
        Description = "Get existing competition league by id",
        OperationId = "GetCompetitionLeague",
        Tags = new[] { "CompetitionLeagues" })]
    public async Task<ActionResult<CompetitionLeagueResource>> GetByIdAsync(int id)
    {
        var competitionLeague = await _competitionLeagueService.FindByIdAsync(id);
        if (competitionLeague == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<CompetitionLeague, CompetitionLeagueResource>(competitionLeague);
        return Ok(resource);
    }
   

    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create new competition league",
        Description = "Create new competition league",
        OperationId = "CreateCompetitionLeague",
        Tags = new[] { "CompetitionLeagues" })]
    public async Task<ActionResult<CompetitionLeagueResource>> PostAsync([FromBody] SaveCompetitionLeagueResource resource, int leagueId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        var competitionLeague = _mapper.Map<SaveCompetitionLeagueResource, CompetitionLeague>(resource);
        var result = await _competitionLeagueService.AddAsync(competitionLeague, leagueId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var competitionLeagueResource = _mapper.Map<CompetitionLeague, CompetitionLeagueResource>(result.Resource);
        return Ok(competitionLeagueResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update existing competition league",
        Description = "Update existing competition league",
        OperationId = "UpdateCompetitionLeague",
        Tags = new[] { "CompetitionLeagues" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCompetitionLeagueResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }

        var competitionLeague = _mapper.Map<CompetitionLeague>(resource);
        var result = await _competitionLeagueService.Update(competitionLeague, id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var competitionLeagueResource = _mapper.Map<CompetitionLeagueResource>(result.Resource);
        return Ok(competitionLeagueResource);
    }
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete existing competition league",
        Description = "Delete existing competition league",
        OperationId = "DeleteCompetitionLeague",
        Tags = new[] { "CompetitionLeagues" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _competitionLeagueService.Delete(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var competitionLeagueResource = _mapper.Map<CompetitionLeagueResource>(result.Resource);
        return Ok(competitionLeagueResource);
    }
}