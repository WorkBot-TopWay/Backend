using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TopWay.API.Security.Authorization.Attributes;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Services;
using TopWay.API.Security.Domain.Services.Communication;
using TopWay.API.Security.Resources;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.Security.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, Read, Update and Delete a Scalers")]
public class ScalersController : ControllerBase
{
    private readonly IScalerService _scalerService;
    private readonly IMapper _mapper;
    
    
    public ScalersController(IScalerService categoryService, IMapper mapper)
    {
        _scalerService = categoryService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The category or climbing gym is invalid")]
    [SwaggerOperation(
        Summary = "Get a user using their credentials",
        Description = "Get a user using their credentials",
        OperationId = "SignIn",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _scalerService.AuthenticateAsync(request);
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Create a new user",
        Description = "Create a new user",
        OperationId = "SignUp",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _scalerService.RegisterAsync(request);
        return Ok(new { message = "Scaler registered successfully" });
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all scalers",
        Description = "Get all scalers",
        OperationId = "GetAll",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> GetAll()
    {
        var response = await _scalerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Scaler>, IEnumerable<ScalerResource>>(response);
        return Ok(resources);
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a scaler by id",
        Description = "Get a scaler by id",
        OperationId = "GetById",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _scalerService.FindByIdAsync(id);
        var resource = _mapper.Map<Scaler, ScalerResource>(response);
        return Ok(resource);
    }
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a scaler",
        Description = "Update a scaler",
        OperationId = "Update",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _scalerService.UpdateAsync(id, request);
        return Ok(new { message = "Scaler updated successfully" });
    }
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a scaler",
        Description = "Delete a scaler",
        OperationId = "Delete",
        Tags = new[] { "Scalers" })]
    public async Task<IActionResult> Delete(int id)
    {
        await _scalerService.DeleteAsync(id);
        return Ok(new { message = "Scaler deleted successfully" });
    }
}