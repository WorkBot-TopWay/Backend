using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("api/v1/competition-gym-rankings")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Update Competition Gym Ranking")]
public class CompetitionGymRankingsController:ControllerBase
{
    private readonly ICompetitionGymRankingService _competitionGymRankingService;
    private readonly  IMapper _mapper;

    public CompetitionGymRankingsController(ICompetitionGymRankingService competitionGymRankingService, IMapper mapper)
    {
        _competitionGymRankingService = competitionGymRankingService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all competition gym rankings or find by Competition Gym Id and Scaler Id or find by Competition Gym Id",
        Description = "Get all competition gym rankings or find by Competition Gym Id and Scaler Id or find by Competition Gym Id",
        OperationId = "GetCompetitionGymRankings",
        Tags = new[] { "CompetitionGymRankings" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? competitionGymId=null,[FromQuery] string? scalerId=null)
    {
        if (competitionGymId != null && scalerId != null)
        {
            int competitionGymIdInt = int.Parse(competitionGymId);
            int scalerIdInt = int.Parse(scalerId);
            var competitionGymRankings = await _competitionGymRankingService.FindByCompetitionIdAndScalerIdAsync(competitionGymIdInt, scalerIdInt);
            if (competitionGymRankings == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<CompetitionGymRankings, CompetitionGymRankingResource>(competitionGymRankings);
            return Ok(resource);
        }

        if (competitionGymId != null)
        {
            int competitionGymIdInt = int.Parse(competitionGymId);
            var competitionGymRankingB = await _competitionGymRankingService.FindByCompetitionGymIdAsync(competitionGymIdInt);
            if (competitionGymRankingB == null)
            {
                return NotFound();
            }
            var resourcesB = _mapper.Map<IEnumerable<CompetitionGymRankings>, IEnumerable<CompetitionGymRankingResource>>(competitionGymRankingB);
            return Ok(resourcesB);
        }

        var competitionGymRanking = await _competitionGymRankingService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionGymRankings>, IEnumerable<CompetitionGymRankingResource>>(competitionGymRanking);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get competition gym ranking by id",
        Description = "Get existing competition gym ranking by id",
        OperationId = "GetCompetitionGymRankingById",
        Tags = new[] { "CompetitionGymRankings" })]
    public async Task<ActionResult<CompetitionGymRankingResource>> GetAsync(int id)
    {
        var competitionGymRanking = await _competitionGymRankingService.FindByIdAsync(id);
        if (competitionGymRanking == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<CompetitionGymRankings, CompetitionGymRankingResource>(competitionGymRanking);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create competition gym ranking",
        Description = "Create new competition gym ranking",
        OperationId = "CreateCompetitionGymRanking",
        Tags = new[] { "CompetitionGymRankings" })]
    public async Task<ActionResult<CompetitionGymRankingResource>> PostAsync([FromBody] SaveCompetitionGymRankingResource resource, int competitionId, int scalerId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        var competitionGymRanking = _mapper.Map<SaveCompetitionGymRankingResource, CompetitionGymRankings>(resource);
        var result = await _competitionGymRankingService.AddAsync(competitionGymRanking, competitionId, scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var competitionGymRankingResource = _mapper.Map<CompetitionGymRankings, CompetitionGymRankingResource>(result.Resource);
        return Ok(competitionGymRankingResource);
    }
    
    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete competition gym ranking",
        Description = "Delete existing competition gym ranking",
        OperationId = "DeleteCompetitionGymRanking",
        Tags = new[] { "CompetitionGymRankings" })]
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