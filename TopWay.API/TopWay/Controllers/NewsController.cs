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
[SwaggerTag(" Create, Read, Update and Delete News")]
public class NewsController: ControllerBase
{
    private readonly INewsService _newsService;
    private readonly IMapper _mapper;

    public NewsController(INewsService newsService, IMapper mapper)
    {
        _newsService = newsService;
        _mapper = mapper;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary="Get all news or find by climbing gym id",
        Description="Get all news or find by climbing gym id",
        OperationId="GetNews",
        Tags=new[] {"News"})]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? climbingGymId=null)
    {
        if (climbingGymId != null)
        {
            int id = int.Parse(climbingGymId);
            var newsA = await _newsService.FindByClimbingGymIdAsync(id);
            var newsResources = _mapper.Map<IEnumerable<News>, IEnumerable<NewsResource>>(newsA);
            return Ok(newsResources);
        }

        var news = await _newsService.ListAsync();
        var result = _mapper.Map<IEnumerable<News>, IEnumerable<NewsResource>>(news);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get news by id",
        Description = "Get existing news by id",
        OperationId = "GetNewsById",
        Tags = new[] { "News" })]
    public async Task<IActionResult> GetAsync(int id)
    {
        var news = await _newsService.FindByIdAsync(id);
        var result = _mapper.Map<News, NewsResource>(news);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The category is invalid")]
    [SwaggerOperation(
        Summary = "Create news",
        Description = "Create new news",
        OperationId = "CreateNews",
        Tags = new[] { "News" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveNewsResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var news = _mapper.Map<SaveNewsResource, News>(resource);
        var result = await _newsService.AddAsync(news, climbingGymId);

        if (!result.Success)
            return BadRequest(result.Message);

        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update news",
        Description = "Update existing news",
        OperationId = "UpdateNews",
        Tags = new[] { "News" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveNewsResource resource)
    {
        var news = _mapper.Map<SaveNewsResource, News>(resource);
        var result = await _newsService.Update(news,id);

        if (!result.Success)
            return BadRequest(result.Message);

        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete news",
        Description = "Delete existing news",
        OperationId = "DeleteNews",
        Tags = new[] { "News" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _newsService.Delete(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }


}