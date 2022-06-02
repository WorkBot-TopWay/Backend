using System.Dynamic;
using AutoMapper;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
public class ScalerController : ControllerBase
{
    private readonly IScalerService _scalerService;
    private readonly IMapper _mapper;
    
    public ScalerController(IScalerService categoryService, IMapper mapper)
    {
        _scalerService = categoryService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ScalerResource>> GetAllAsync()
    {
        var scalers = await _scalerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(scalers);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var scaler = await _scalerService.FindByIdAsync(id);
        if (scaler == null)
            return NotFound();
        
        var resource = _mapper.Map<Scaler, ScalerResource>(scaler);
        return Ok(resource);
    }
    
    [HttpPost]
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _scalerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var scalerResource = _mapper.Map<Scaler, ScalerResource>(result.Resource);
        return Ok(scalerResource);
    }
}