using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Update competition league rankings")]
public class CompetitionLeagueRankingsController:ControllerBase
{
    private readonly ICompetitionLeagueRankingService _competitionLeagueRankingService;
    private readonly IMapper _mapper;

    public CompetitionLeagueRankingsController(IMapper mapper, ICompetitionLeagueRankingService competitionLeagueRankingService)
    {
        _mapper = mapper;
        _competitionLeagueRankingService = competitionLeagueRankingService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all competition league rankings or find by competition league id and scale id or find by competition league id",
        Description = "Get all competition league rankings or find by competition league id and scale id or find by competition league id",
        OperationId = "GetCompetitionLeagueRankings",
        Tags = new[] { "CompetitionLeagueRankings" })]
    public async Task<IActionResult> ListAsync([FromQuery] string ? competitionLeagueId =null,[FromQuery] string ? scalerId =null) 
    {
        if(competitionLeagueId !=null && scalerId !=null)
        {
           int competitionLeagueIdInt = int.Parse(competitionLeagueId);
           int scalerIdInt = int.Parse(scalerId);
           var competitionLeagueRankings = await _competitionLeagueRankingService.FindByCompetitionLeagueIdAndScalerIdAsync(competitionLeagueIdInt, scalerIdInt);
           if (competitionLeagueRankings == null)
               return NotFound();
           var resource = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(competitionLeagueRankings);
           return Ok(resource);
        }

        if (competitionLeagueId != null)
        {
            int competitionLeagueIdInt = int.Parse(competitionLeagueId);
            var competitionLeagueRankingB = await _competitionLeagueRankingService.FindByCompetitionLeagueIdAsync(competitionLeagueIdInt);
            var resourcesB = _mapper.Map<IEnumerable<CompetitionLeagueRanking>, IEnumerable<CompetitionLeagueRankingResource>>(competitionLeagueRankingB);
            return Ok(resourcesB);
        }

        var competitionLeagueRanking = await _competitionLeagueRankingService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionLeagueRanking>, IEnumerable<CompetitionLeagueRankingResource>>(competitionLeagueRanking);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get competition league ranking by id",
        Description = "Get existing competition league ranking by id",
        OperationId = "GetCompetitionLeagueRankingById",
        Tags = new[] { "CompetitionLeagueRankings" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionLeagueRanking = await _competitionLeagueRankingService.FindByIdAsync(id);
        if (competitionLeagueRanking == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(competitionLeagueRanking);
        return Ok(resources);
    }
   
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create competition league ranking",
        Description = "Create new competition league ranking",
        OperationId = "CreateCompetitionLeagueRanking",
        Tags = new[] { "CompetitionLeagueRankings" })]
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
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Update competition league ranking",
        Description = "Update existing competition league ranking",
        OperationId = "UpdateCompetitionLeagueRanking",
        Tags = new[] { "CompetitionLeagueRankings" })]
    public async Task<IActionResult> PutAsync([FromBody] SaveCompetitionLeagueRankingResource resource, int competitionLeagueId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionLeagueRanking = _mapper.Map<SaveCompetitionLeagueRankingResource, CompetitionLeagueRanking>(resource);
        var result = await _competitionLeagueRankingService.Update(competitionLeagueRanking, competitionLeagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionLeagueRankingResource = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(result.Resource);
        return Ok(competitionLeagueRankingResource);
    }
        
    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete competition league ranking",
        Description = "Delete existing competition league ranking",
        OperationId = "DeleteCompetitionLeagueRanking",
        Tags = new[] { "CompetitionLeagueRankings" })]
    public async Task<IActionResult> DeleteAsync(int competitionLeagueId, int scalerId)
    {
        var result = await _competitionLeagueRankingService.Delete(competitionLeagueId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionLeagueRankingResource = _mapper.Map<CompetitionLeagueRanking, CompetitionLeagueRankingResource>(result.Resource);
        return Ok(competitionLeagueRankingResource);
    }
}