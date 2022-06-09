using System.Dynamic;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag(" Create, Read, Update and Delete a Scalers")]
public class ScalersController : ControllerBase
{
    private readonly IScalerService _scalerService;
    private readonly IMapper _mapper;
    
    public ScalersController(IScalerService categoryService, IMapper mapper)
    {
        _scalerService = categoryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers or a specific one by email and password",
        Description = "Get all scalers or a specific one by email and password",
        OperationId = "GetScalers",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string ?email=null,[FromQuery] string ?password=null)
   
    {
        if (email != null && password != null)
        {
            var scaler = await _scalerService.FindByIdEmailAndPasswordAsync(email, password);
            var scalerResource = _mapper.Map<Scaler, ScalerResource>(scaler);
            return Ok(scalerResource);
        }

        var scalers = await _scalerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scalers);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a scaler",
        Description = "Get existing scaler",
        OperationId = "GetScaler",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> GetAsync(int id)
    {
        var scaler = await _scalerService.FindByIdAsync(id);
        if (scaler == null)
            return NotFound();
        
        var resource = _mapper.Map<Scaler, ScalerResource>(scaler);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a scaler",
        Description = "Create a new scaler",
        OperationId = "CreateScaler",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveScalerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var scaler = _mapper.Map<SaveScalerResource, Scaler>(resource);
        var result = await _scalerService.SaveAsync(scaler);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var scalerResource = _mapper.Map<Scaler, ScalerResource>(result.Resource);
        return Ok(scalerResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a scaler",
        Description = "Update an existing scaler",
        OperationId = "UpdateScaler",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveScalerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var scaler = _mapper.Map<SaveScalerResource, Scaler>(resource);
        var result = await _scalerService.UpdateAsync(id, scaler);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var scalerResource = _mapper.Map<Scaler, ScalerResource>(result.Resource);
        return Ok(scalerResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a scaler",
        Description = "Delete an existing scaler",
        OperationId = "DeleteScaler",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _scalerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var scalerResource = _mapper.Map<Scaler, ScalerResource>(result.Resource);
        return Ok(scalerResource);
    }
}