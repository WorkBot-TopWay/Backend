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
public class CompetitionLeagueController : ControllerBase
{
    private readonly ICompetitionLeagueService _competitionLeagueService;
    private readonly IMapper _mapper;

    public CompetitionLeagueController(ICompetitionLeagueService competitionLeagueService, IMapper mapper)
    {
        _competitionLeagueService = competitionLeagueService;
        _mapper = mapper;
    }


    [HttpGet]
    public Task<IEnumerable<CompetitionLeagueResource>> GetAllAsync()
    {
        var competitionLeagues = _competitionLeagueService.ListAsync();
        var resources = competitionLeagues.Result.Select(x => _mapper.Map<CompetitionLeagueResource>(x));
        return Task.FromResult(resources);
    }
    
    [HttpGet("{id}")]
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
   

    [HttpGet("FindByLeagueIdAsync")]
    public async Task<ActionResult<IEnumerable<CompetitionLeagueResource>>>  FindByLeagueIdAsync(int leagueId)
    {
        var competitionLeagues = await _competitionLeagueService.FindByLeagueIdAsync(leagueId);
        var resources = competitionLeagues.Select(x => _mapper.Map<CompetitionLeagueResource>(x));
        return Ok(resources);
    }
    [HttpPost]
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