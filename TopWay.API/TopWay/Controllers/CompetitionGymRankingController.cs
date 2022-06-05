using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class CompetitionGymRankingController:ControllerBase
{
    private readonly ICompetitionGymRankingService _competitionGymRankingService;
    private readonly  IMapper _mapper;

    public CompetitionGymRankingController(ICompetitionGymRankingService competitionGymRankingService, IMapper mapper)
    {
        _competitionGymRankingService = competitionGymRankingService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CompetitionGymRankingResource>> GetAllAsync()
    {
        var competitionGymRanking = await _competitionGymRankingService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionGymRanking>, IEnumerable<CompetitionGymRankingResource>>(competitionGymRanking);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionGymRankingResource>> GetAsync(int id)
    {
        var competitionGymRanking = await _competitionGymRankingService.FindByIdAsync(id);
        if (competitionGymRanking == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<CompetitionGymRanking, CompetitionGymRankingResource>(competitionGymRanking);
        return Ok(resource);
    }
    
    [HttpGet("FindByCompetitionIdAndScalerId")]
    public async Task<ActionResult<CompetitionGymRankingResource>> FindByCompetitionIdAndScalerId(int competitionId, int scalerId)
    {
        var competitionGymRanking = await _competitionGymRankingService.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        if (competitionGymRanking == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<CompetitionGymRanking, CompetitionGymRankingResource>(competitionGymRanking);
        return Ok(resource);
    }
    
    [HttpGet("FindScalerByCompetitionId")]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindScalerByCompetitionId(int competitionId)
    {
        var scaler = await _competitionGymRankingService.FindScalerByCompetitionIdAsync(competitionId);
        if (scaler == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return Ok(resource);
    }
    
    [HttpPost]
    public async Task<ActionResult<CompetitionGymRankingResource>> PostAsync([FromBody] SaveCompetitionGymRankingResource resource, int competitionId, int scalerId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        var competitionGymRanking = _mapper.Map<SaveCompetitionGymRankingResource, CompetitionGymRanking>(resource);
        var result = await _competitionGymRankingService.AddAsync(competitionGymRanking, competitionId, scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var competitionGymRankingResource = _mapper.Map<CompetitionGymRanking, CompetitionGymRankingResource>(result.Resource);
        return Ok(competitionGymRankingResource);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int competitionId, int scalerId)
    {
        var result = await _competitionGymRankingService.Delete(competitionId, scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }
    
}