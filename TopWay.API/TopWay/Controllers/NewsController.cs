using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsServices _newsServices;
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
}