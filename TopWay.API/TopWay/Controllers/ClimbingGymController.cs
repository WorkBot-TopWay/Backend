using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class ClimbingGymController : ControllerBase
{
    private readonly IClimbingGymService _climbingGymService;
    private readonly IMapper _mapper;
    
    public ClimbingGymController(IClimbingGymService climbingGymService, IMapper mapper)
    {
        _climbingGymService = climbingGymService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ClimbingGymResource>> GetAllAsync()
    {
        var climbingGyms = await _climbingGymService.ListAsync();
        var resources = _mapper.Map<IEnumerable<ClimbingGym>, IEnumerable<ClimbingGymResource>>(climbingGyms);
        return resources;
    }
    
    [HttpGet("findByName/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var climbingGym = await _climbingGymService.FindByNameAsync(name);
        if (climbingGym == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<ClimbingGym, ClimbingGymResource>(climbingGym);
        return Ok(resource);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var climbingGym = await _climbingGymService.FindByIdAsync(id);
        if (climbingGym == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<ClimbingGym, ClimbingGymResource>(climbingGym);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveClimbingGymResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var climbingGym = _mapper.Map<SaveClimbingGymResource, ClimbingGym>(resource);
        var result = await _climbingGymService.SaveAsync(climbingGym);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGym, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClimbingGymResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
        var climbingGym = _mapper.Map<SaveClimbingGymResource, ClimbingGym>(resource);
        var result = await _climbingGymService.UpdateAsync(id, climbingGym);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGym, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _climbingGymService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var climbingGymResource = _mapper.Map<ClimbingGym, ClimbingGymResource>(result.Resource);
        return Ok(climbingGymResource);
    }
}