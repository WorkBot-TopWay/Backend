using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag(" Create, Read and Delete a Requests")]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public RequestsController(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all requests and find by scaler id and league id",
        Description = "Get all requests",
        OperationId = "GetAllRequests",
        Tags = new[] { "Requests" })]
    public async  Task<IActionResult> GetAllAsync([FromQuery]string? scalerId=null, [FromQuery]string?leagueId=null)
    {
        if (scalerId != null && leagueId!=null)
        {
            int idScaler = int.Parse(scalerId);
            int idLeague = int.Parse(leagueId);
            var request = await _requestService.FindLeagueIdAndScapeId(idLeague, idScaler);
            if (request == null)
                return NotFound();
            var resource = _mapper.Map<Request, RequestResource>(request);
            return Ok(resource);
        }
        
        var requests = await _requestService.GetAll();
        var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get request by id",
        Description = "Get existing request by id",
        OperationId = "GetRequestById",
        Tags = new[] { "Requests" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var request = await _requestService.FindByIdAsync(id);
        if (request == null)
            return NotFound();
        var resource = _mapper.Map<Request, RequestResource>(request);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create request",
        Description = "Create new request",
        OperationId = "CreateRequest",
        Tags = new[] { "Requests" })]
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
    [SwaggerOperation(
        Summary = "Delete request",
        Description = "Delete existing request",
        OperationId = "DeleteRequest",
        Tags = new[] { "Requests" })]
    public async Task<IActionResult> DeleteAsync(int leagueId, int scalerId)
    {
        var result = await _requestService.Delete(leagueId, scalerId);

        if (!result.Success)
            return BadRequest(result.Message);

        var requestResource = _mapper.Map<Request, RequestResource>(result.Resource);
        return Ok(requestResource);
    }
}