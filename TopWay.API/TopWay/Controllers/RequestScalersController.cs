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
[Route("api/v1/requests/{leagueId}/scalers")]
[Produces("application/json")]
public class RequestScalersController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public RequestScalersController(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers for a request",
        Description = "Get all scalers for a request",
        OperationId = "GetRequestScalers",
        Tags = new[] { "Requests" })]
    public async Task<ActionResult<IEnumerable<Scaler>>> FindRequestScalersByLeagueId(int leagueId)
    {
        var request = await _requestService.FindRequestScalerByLeagueId(leagueId);
        if (request == null)
            return NotFound();
        var resource = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(request);
        return Ok(resource);
    }

}