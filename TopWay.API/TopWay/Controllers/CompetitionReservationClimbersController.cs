using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/competition-reservation-climbers")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Update Competition Reservation Climbers")]
public class CompetitionReservationClimbersController:ControllerBase
{
    private readonly ICompetitionReservationClimberService _competitionReservationClimberService;
    private readonly IMapper _mapper;


    public CompetitionReservationClimbersController(ICompetitionReservationClimberService competitionReservationClimberService, IMapper mapper)
    {
        _competitionReservationClimberService = competitionReservationClimberService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all competition reservation climbers or find competition gym id and scape id or find by competition gym id",
        Description = "Get all competition reservation climbers or find competition gym id and scape id or find by competition gym id",
        OperationId = "GetCompetitionReservationClimbers",
        Tags = new[] { "CompetitionReservationClimbers" })]
    public async Task<IActionResult> ListAsync([FromQuery] string ? competitionGymId=null, [FromQuery] string ? scalerId=null)
    {
        if(competitionGymId != null && scalerId != null)
        {
            int competitionId = int.Parse(competitionGymId);
            int ScalerId = int.Parse(scalerId);
            var result = await _competitionReservationClimberService.FindByCompetitionIdAndScalerIdAsync(competitionId, ScalerId);
            var resource = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(result);
            return Ok(resource);
        }

        if (competitionGymId != null)
        {
            int competitionId = int.Parse(competitionGymId);
            var results = await _competitionReservationClimberService.FindByCompetitionIdAsync(competitionId);
            var resource = _mapper.Map<IEnumerable<CompetitionReservationClimber>, IEnumerable<CompetitionReservationClimberResource>>(results);
        }

        var competitionReservationClimber = await _competitionReservationClimberService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionReservationClimber>, IEnumerable<CompetitionReservationClimberResource>>(competitionReservationClimber);
        return Ok(resources);
    }
    
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get competition reservation climber by id",
        Description = "Get existing competition reservation climber by id",
        OperationId = "GetCompetitionReservationClimber",
        Tags = new[] { "CompetitionReservationClimbers" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionReservationClimber = await _competitionReservationClimberService.FindByIdAsync(id);
        if (competitionReservationClimber == null)
            return NotFound();
        var resources = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(competitionReservationClimber);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create new competition reservation climber",
        Description = "Create new competition reservation climber",
        OperationId = "CreateCompetitionReservationClimber",
        Tags = new[] { "CompetitionReservationClimbers" })]
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
    [SwaggerOperation(
        Summary = "Delete competition reservation climber by id",
        Description = "Delete existing competition reservation climber by id",
        OperationId = "DeleteCompetitionReservationClimber",
        Tags = new[] { "CompetitionReservationClimbers" })]
    public async Task<IActionResult> DeleteAsync(int competitionId, int scalerId)
    {
        var result = await _competitionReservationClimberService.Delete(competitionId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionReservationClimberResource = _mapper.Map<CompetitionReservationClimber, CompetitionReservationClimberResource>(result.Resource);
        return Ok(competitionReservationClimberResource);
    }

}