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
[Route("api/v1/competition-gym-rankings/{competitiongymId}/scalers")]
[Produces("application/json")]
public class CompetitionGymRankingScalersController:ControllerBase
{
    private readonly ICompetitionGymRankingService _competitionGymRankingService;
    private readonly  IMapper _mapper;

    public CompetitionGymRankingScalersController(ICompetitionGymRankingService competitionGymRankingService, IMapper mapper)
    {
        _competitionGymRankingService = competitionGymRankingService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers for a competition gym ranking",
        Description = "Get all scalers for a competition gym ranking",
        OperationId = "GetCompetitionGymRankingScalers",
        Tags = new[] { "CompetitionGymRankings" })]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindScalerByCompetitionId(int competitiongymId)
    {
        var scaler = await _competitionGymRankingService.FindScalerByCompetitionIdAsync(competitiongymId);
        if (scaler == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return Ok(resource);
    }
}