using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class CompetitionReservationClimberController:ControllerBase
{
    private readonly ICompetitionReservationClimberService _competitionReservationClimberService;
    private readonly IMapper _mapper;


    public CompetitionReservationClimberController(ICompetitionReservationClimberService competitionReservationClimberService, IMapper mapper)
    {
        _competitionReservationClimberService = competitionReservationClimberService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    public async Task<IEnumerable<CompetitionReservationClimberResource>> ListAsync()
    {
        var competitionReservationClimber = await _competitionReservationClimberService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionReservationClimber>, IEnumerable<CompetitionReservationClimberResource>>(competitionReservationClimber);
        return resources;
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionReservationClimber = await _competitionReservationClimberService.FindByIdAsync(id);
        if (competitionReservationClimber == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(competitionReservationClimber);
        return Ok(resources);
    }
    
    
    [HttpGet("FindScalerByCompetitionId")]
    public async Task<IEnumerable<ScalerResource>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        var scaler = await _competitionReservationClimberService.FindScalerByCompetitionIdAsync(competitionId);
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return resources;
    }
    [HttpGet("FindByCompetitionIdAndScalerId")]
    public async Task<IActionResult> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        var competitionReservationClimber = await _competitionReservationClimberService.FindByCompetitionIdAndScalerIdAsync(competitionId, scalerId);
        if (competitionReservationClimber == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(competitionReservationClimber);
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCompetitionReservationClimberResource resource,int competitionId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionReservationClimber = _mapper.Map<SaveCompetitionReservationClimberResource,CompetitionReservationClimber>(resource);
        var result = await _competitionReservationClimberService.AddAsync(competitionReservationClimber, competitionId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionReservationClimberResource = _mapper.Map<CompetitionReservationClimber,CompetitionReservationClimberResource>(result.Resource);
        return Ok(competitionReservationClimberResource);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int competitionId, int scalerId)
    {
        var result = await _competitionReservationClimberService.Delete(competitionId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionReservationClimberResource = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(result.Resource);
        return Ok(competitionReservationClimberResource);
    }

}