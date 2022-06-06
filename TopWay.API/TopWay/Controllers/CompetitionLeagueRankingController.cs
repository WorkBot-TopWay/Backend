using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CompetitionLeagueRankingController:ControllerBase
{
    private readonly ICompetitionLeagueRankingService _competitionLeagueRankingService;
    private readonly IMapper _mapper;

    public CompetitionLeagueRankingController(IMapper mapper, ICompetitionLeagueRankingService competitionLeagueRankingService)
    {
        _mapper = mapper;
        _competitionLeagueRankingService = competitionLeagueRankingService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CompetitionLeagueRankingResource>> ListAsync()
    {
        var competitionLeagueRanking = await _competitionLeagueRankingService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionLeagueRanking>, IEnumerable<CompetitionLeagueRankingResource>>(competitionLeagueRanking);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionLeagueRanking = await _competitionLeagueRankingService.FindByIdAsync(id);
        if (competitionLeagueRanking == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(competitionLeagueRanking);
        return Ok(resources);
    }
    
    [HttpGet("FindScalerByCompetitionId")]
    public async Task<IEnumerable<ScalerResource>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        var scaler = await _competitionLeagueRankingService.FindScalerByCompetitionIdAsync(competitionId);
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return resources;
    }
    
    [HttpGet("FindByCompetitionLeagueId")]
    public async Task<IEnumerable<CompetitionLeagueRankingResource>> FindByCompetitionLeagueIdAsync(int competitionLeagueId)
    {
        var competitionLeagueRanking = await _competitionLeagueRankingService.FindByCompetitionLeagueIdAsync(competitionLeagueId);
        var resources = _mapper.Map<IEnumerable<CompetitionLeagueRanking>, IEnumerable<CompetitionLeagueRankingResource>>(competitionLeagueRanking);
        return resources;
    }
    
    [HttpGet("FindByCompetitionLeagueIdAndScalerI")]
    public async  Task<IActionResult> FindByCompetitionLeagueIdAndScalerIAsync(int competitionLeagueId, int scalerI)
    {
        var competitionLeagueRanking = await _competitionLeagueRankingService.FindByCompetitionLeagueIdAndScalerIdAsync(competitionLeagueId, scalerI);
        if (competitionLeagueRanking == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(competitionLeagueRanking);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCompetitionLeagueRankingResource resource, int competitionLeagueId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionLeagueRanking = _mapper.Map<SaveCompetitionLeagueRankingResource, CompetitionLeagueRanking>(resource);
        var result = await _competitionLeagueRankingService.AddAsync(competitionLeagueRanking, competitionLeagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionLeagueRankingResource = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(result.Resource);
        return Ok(competitionLeagueRankingResource);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int competitionLeagueId, int scalerId)
    {
        var result = await _competitionLeagueRankingService.Delete(competitionLeagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionLeagueRankingResource = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(result.Resource);
        return Ok(competitionLeagueRankingResource);
    }
}