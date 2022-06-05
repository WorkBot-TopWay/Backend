using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public RequestController(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RequestResource>> GetAllAsync()
    {
        var requests = await _requestService.GetAll();
        var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var request = await _requestService.FindByIdAsync(id);
        if (request == null)
            return NotFound();
        var resource = _mapper.Map<Request, RequestResource>(request);
        return Ok(resource);
    }

    [HttpGet("findByLeagueIdAndScalerId")]
    public async Task<IActionResult> FindByLeagueIdAndScalerId(int leagueId, int scalerId)
    {
        var request = await _requestService.FindLeagueIdAndScapeId(leagueId, scalerId);
        if (request == null)
            return NotFound();
        var resource = _mapper.Map<Request, RequestResource>(request);
        return Ok(resource);
    }

    [HttpGet("findRequestScalersByLeagueId")]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindRequestScalersByLeagueId(int leagueId)
    {
        var request = await _requestService.FindRequestScalerByLeagueId(leagueId);
        if (request == null)
            return NotFound();
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(request);
        return Ok(resource);
    }
        
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveRequestResource resource, int leagueId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var request = _mapper.Map<SaveRequestResource, Request>(resource);
        var result = await _requestService.AddAsync(request, leagueId, scalerId);

        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);
        return Ok(requestResource);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int leagueId, int scalerId)
    {
        var result = await _requestService.Delete(leagueId, scalerId);

        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);
        return Ok(requestResource);
    }
}