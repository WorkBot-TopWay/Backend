using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("/api/v1/climbing-gyms")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete Climbing Gym")]

public class ClimbingGymsController : ControllerBase
{
    private readonly IClimbingGymService _climbingGymService;
    private readonly IMapper _mapper;
    
    public ClimbingGymsController(IClimbingGymService climbingGymService, IMapper mapper)
    {
        _climbingGymService = climbingGymService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Climbing Gyms or a specific Climbing Gym by name",
        Description = "Get all existing Climbing Gyms or a specific Climbing Gym by name",
        OperationId = "GetClimbingGym",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> GetAllAsync([FromQuery]string? name=null)
    {
        if (name != null)
        {
            var climbingGym = await _climbingGymService.FindByNameAsync(name);
            if (climbingGym == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(climbingGym);
            return Ok(resource);
        }

        var climbingGyms = await _climbingGymService.ListAsync();
        var resources = _mapper.Map<IEnumerable<ClimbingGyms>, IEnumerable<ClimbingGymResource>>(climbingGyms);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a specific Climbing Gym by id",
        Description = "Get a existing Climbing Gym by id",
        OperationId = "GetClimbingGymById",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> GetAsync(int id)
    {
        var climbingGym = await _climbingGymService.FindByIdAsync(id);
        if (climbingGym == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(climbingGym);
        return Ok(resource);
    }
    [HttpGet("login")]
    [SwaggerOperation(
        Summary = "Get a specific Climbing Gym by login",
        Description = "Get a existing Climbing Gym by login",
        OperationId = "GetClimbingGymByLogin",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> LogInAsync(string email, string password)
    {
        var climbingGym = await _climbingGymService.LogIn(email, password);
        if (climbingGym == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(climbingGym);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a Climbing Gym",
        Description = "Create a new Climbing Gym",
        OperationId = "CreateClimbingGym",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveClimbingGymResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var climbingGym = _mapper.Map<SaveClimbingGymResource, ClimbingGyms>(resource);
        var result = await _climbingGymService.SaveAsync(climbingGym);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a Climbing Gym",
        Description = "Update a existing Climbing Gym",
        OperationId = "UpdateClimbingGym",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClimbingGymResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var climbingGym = _mapper.Map<SaveClimbingGymResource, ClimbingGyms>(resource);
        var result = await _climbingGymService.UpdateAsync(id, climbingGym);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Climbing Gym",
        Description = "Delete a existing Climbing Gym",
        OperationId = "DeleteClimbingGym",
        Tags = new[] { "ClimbingGyms" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _climbingGymService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGyms, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
}