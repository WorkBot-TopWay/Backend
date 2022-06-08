using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;
using MySqlX.XDevAPI.Common;
using TopWay.API.Shared.Extensions;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsServices _newsServices;
    private readonly IClimbingGymService _climbingGymService;
    private readonly IMapper _mapper;

    public NewsController(INewsServices newsServices, IMapper mapper)
    {
        _mapper = mapper;
        _newsServices = newsServices;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NewsResource>> GetAllAsync()
    {
        var news = await _newsServices.ListAsync();
        var resources = _mapper.Map<IEnumerable<News>, IEnumerable<NewsResource>>(news);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<NewsResource>> GetAsync(int id)
    {
        var news = await _newsServices.FindByIdAsync(id);
        if (news == null)
        {
            return NotFound();
        }

        var resource = _mapper.Map<News, NewsResource>(news);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult> PostASync([FromBody] SaveNewsResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        
         
        var news = _mapper.Map<SaveNewsResource, News>(resource);
        var result = await _newsServices.SaveAsync(news, climbingGymId);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(int id, [FromBody] SaveNewsResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }

        var news = _mapper.Map<SaveNewsResource, News>(resource);
        var result = await _newsServices.UpdateAsync(news, id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _newsServices.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var newsResource = _mapper.Map<News, NewsResource>(result.Resource);
        return Ok(newsResource);
    }
}