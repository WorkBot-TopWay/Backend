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
public class CompetitionGymController : ControllerBase
{
    private readonly ICompetitionGymService _competitionGymService;
    private readonly IMapper _mapper;

    public CompetitionGymController(ICompetitionGymService competitionGymService, IMapper mapper)
    {
        _competitionGymService = competitionGymService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CompetitionGymResource>> ListAsync()
    {
        var competitionGym = await _competitionGymService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionGym>, IEnumerable<CompetitionGymResource>>(competitionGym);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionGym = await _competitionGymService.FindByIdAsync(id);
        if (competitionGym == null)
            return NotFound();
        var resource = _mapper.Map<CompetitionGym, CompetitionGymResource>(competitionGym);
        return Ok(resource);
    }

    [HttpGet("findByClimbingGymId/{id}")]
    public async Task<IEnumerable<CompetitionGymResource>> FindByClimbingGymIdAsync(int id)
    {
        var competitionGym = await _competitionGymService.FindByClimbingGymIdAsync(id);
        var resources = _mapper.Map<IEnumerable<CompetitionGym>, IEnumerable<CompetitionGymResource>>(competitionGym);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCompetitionGymResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionGym = _mapper.Map<SaveCompetitionGymResource, CompetitionGym>(resource);
        var result = await _competitionGymService.SaveAsync(competitionGym, climbingGymId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGym, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCompetitionGymResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionGym = _mapper.Map<SaveCompetitionGymResource, CompetitionGym>(resource);
        var result = await _competitionGymService.UpdateAsync(id, competitionGym);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGym, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _competitionGymService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGym, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

}