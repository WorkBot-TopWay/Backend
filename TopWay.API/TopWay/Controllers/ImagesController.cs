using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ImagesController: ControllerBase
{
    private readonly IImagesService _imageService;
    private readonly IMapper _mapper;

    public ImagesController(IImagesService imageService, IMapper mapper)
    {
        _imageService = imageService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ImagesResource>> GetAllAsync()
    {
        var images = await _imageService.FindAllAsync();
        var resources = _mapper.Map<IEnumerable<Images>, IEnumerable<ImagesResource>>(images);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var image = await _imageService.FindByIdAsync(id);
        if (image == null)
            return NotFound();
        var resources = _mapper.Map<Images, ImagesResource>(image);
        return Ok(resources);
    }
    
    [HttpGet("findByClimbingGymId/{id}")]
    public async Task<IEnumerable<ImagesResource>> GetByClimbingGymIdAsync(int id)
    {
        var images = await _imageService.FindByClimbingGymIdAsync(id);
        var resources = _mapper.Map<IEnumerable<Images>, IEnumerable<ImagesResource>>(images);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveImagesResource resource, int climbingGymId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var image = _mapper.Map<SaveImagesResource, Images>(resource);
        var result = await _imageService.SaveAsync(image, climbingGymId);
        if (!result.Success)
            return BadRequest(result.Message);
        var imageResource = _mapper.Map<Images, ImagesResource>(result.Resource);
        return Ok(imageResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _imageService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var imageResource = _mapper.Map<Images, ImagesResource>(result.Resource);
        return Ok(imageResource);
    }

}