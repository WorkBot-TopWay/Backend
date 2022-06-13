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
[Route("/api/v1/competition-leagues/{competitiongymId}/scalers")]
[Produces("application/json")]
public class CompetitionReservationClimberScalersController:ControllerBase
{
    private readonly ICompetitionReservationClimberService _competitionReservationClimberService;
    private readonly IMapper _mapper;


    public CompetitionReservationClimberScalersController(ICompetitionReservationClimberService competitionReservationClimberService, IMapper mapper)
    {
        _competitionReservationClimberService = competitionReservationClimberService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers of a competition gym",
        Description = "Get all scalers of a competition gym",
        OperationId = "GetCompetitionReservationClimberScalers",
        Tags = new[] { "CompetitionReservationClimbers" })]
    public async Task<IEnumerable<ScalerResource>> FindScalerByCompetitionIdAsync(int competitiongymId)
    {
        var scaler = await _competitionReservationClimberService.FindScalerByCompetitionIdAsync(competitiongymId);
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scaler);
        return resources;
    }
}