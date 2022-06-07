using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/competition-gyms")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete a competition gym")]
public class CompetitionGymsController : ControllerBase
{
    private readonly ICompetitionGymService _competitionGymService;
    private readonly IMapper _mapper;

    public CompetitionGymsController(ICompetitionGymService competitionGymService, IMapper mapper)
    {
        _competitionGymService = competitionGymService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all competition gym or find by Climbing Gym Id",
        Description = "Get all competition gym or find by Climbing Gym Id",
        OperationId = "GetCompetitionGym",
        Tags = new[] { "CompetitionGyms" })]
    public async Task<IActionResult> ListAsync([FromQuery] string? climbingGymId= null)
    {
        if (climbingGymId != null)
        {
            int ClimbGymId = int.Parse(climbingGymId);
            var competitionGyms = await _competitionGymService.FindByClimbingGymIdAsync(ClimbGymId);
            var resource = _mapper.Map<IEnumerable<CompetitionGyms>, IEnumerable<CompetitionGymResource>>(competitionGyms);
            return Ok(resource);
        }

        var competitionGym = await _competitionGymService.ListAsync();
        var resources = _mapper.Map<IEnumerable<CompetitionGyms>, IEnumerable<CompetitionGymResource>>(competitionGym);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a competition gym by Id",
        Description = "Get a existing competition gym by Id",
        OperationId = "GetCompetitionGymById",
        Tags = new[] { "CompetitionGyms" })]
   
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var competitionGym = await _competitionGymService.FindByIdAsync(id);
        if (competitionGym == null)
            return NotFound();
        var resource = _mapper.Map<CompetitionGyms, CompetitionGymResource>(competitionGym);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a competition gym",
        Description = "Create a new competition gym",
        OperationId = "CreateCompetitionGym",
        Tags = new[] { "CompetitionGyms" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveCompetitionGymResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionGym = _mapper.Map<SaveCompetitionGymResource, CompetitionGyms>(resource);
        var result = await _competitionGymService.SaveAsync(competitionGym, climbingGymId);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGyms, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a competition gym",
        Description = "Update a existing competition gym",
        OperationId = "UpdateCompetitionGym",
        Tags = new[] { "CompetitionGyms" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCompetitionGymResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var competitionGym = _mapper.Map<SaveCompetitionGymResource, CompetitionGyms>(resource);
        var result = await _competitionGymService.UpdateAsync(id, competitionGym);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGyms, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a competition gym",
        Description = "Delete a existing competition gym",
        OperationId = "DeleteCompetitionGym",
        Tags = new[] { "CompetitionGyms" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _competitionGymService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var competitionGymResource = _mapper.Map<CompetitionGyms, CompetitionGymResource>(result.Resource);
        return Ok(competitionGymResource);
    }

}