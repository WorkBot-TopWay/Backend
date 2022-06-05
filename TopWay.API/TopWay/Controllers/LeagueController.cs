using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class LeagueController: ControllerBase
{
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;

    public LeagueController(ILeagueService leagueService, IMapper mapper)
    {
        _leagueService = leagueService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<LeagueResource>> GetAllAsync()
    {
        var leagues = await _leagueService.GetAll();
        var resources = _mapper.Map<IEnumerable<League>, IEnumerable<LeagueResource>>(leagues);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var league = await _leagueService.GetById(id);
        if (league == null)
            return NotFound();
        var resource = _mapper.Map<League, LeagueResource>(league);
        return Ok(resource);
    }
    
    [HttpGet("FindByClimbingGymId")]
    public async Task<IEnumerable<LeagueResource>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        var leagues = await _leagueService.FindByClimbingGymId(climbingGymId);
        var resources = _mapper.Map<IEnumerable<League>, IEnumerable<LeagueResource>>(leagues);
        return resources;
    }
    
    [HttpPost]
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
    
    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] SaveLeagueResource league, int leagueId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var leagueResource = _mapper.Map<SaveLeagueResource, League>(league);
        var result = await _leagueService.Update(leagueResource, leagueId);

        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResourceUpdated = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResourceUpdated);
    }
    [HttpPut("AddNewMember")]
    public async Task<IActionResult> AddNewParticipantAsync(int leagueId)
    {
        var result = await _leagueService.AddNewParticipant(leagueId);
        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResource = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResource);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _leagueService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var leagueResource = _mapper.Map<League, LeagueResource>(result.Resource);
        return Ok(leagueResource);
    }
    
}