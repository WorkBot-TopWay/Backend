using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Resources;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/competition-league-rankings/{competitionleagueid}/scalers")]
[Produces("application/json")]
public class CompetitionLeagueRankingScalersController:ControllerBase
{
    private readonly ICompetitionLeagueRankingService _competitionLeagueRankingService;
    private readonly IMapper _mapper;

    public CompetitionLeagueRankingScalersController(IMapper mapper, ICompetitionLeagueRankingService competitionLeagueRankingService)
    {
        _mapper = mapper;
        _competitionLeagueRankingService = competitionLeagueRankingService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers for a competition league ranking",
        Description = "Get all scalers for a competition league ranking",
        OperationId = "GetCompetitionLeagueRankingScalers",
        Tags = new[] { "CompetitionLeagueRankings" })]
    public async Task<ActionResult<ScalerResource>> FindScalerByCompetitionIdAsync(int competitionleagueid)
    {
        var scaler = await _competitionLeagueRankingService.FindScalerByCompetitionIdAsync(competitionleagueid);
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return Ok(resources);
    }
}