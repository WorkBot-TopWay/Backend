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
[SwaggerTag(" Create, Read and Delete a Images")]
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
    [SwaggerOperation(
        Summary = "Get all images",
        Description = "Get all images",
        OperationId = "GetAllImages",
        Tags = new[] { "Images" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string ? climbingGymId=null)
    {
        if (climbingGymId != null)
        {
            int id = int.Parse(climbingGymId);
            var image = await _imageService.FindByClimbingGymIdAsync(id);
            var resource = _mapper.Map<IEnumerable<Images>, IEnumerable<ImagesResource>>(image);
            return Ok(resource);
        }
        var images = await _imageService.FindAllAsync();
        var resources = _mapper.Map<IEnumerable<Images>, IEnumerable<ImagesResource>>(images);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary ="Get image by id",
        Description = "Get existing image by id",
        OperationId = "GetImageById",
        Tags = new[] { "Images" })]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var image = await _imageService.FindByIdAsync(id);
        if (image == null)
            return NotFound();
        var resources = _mapper.Map<Images, ImagesResource>(image);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create new image",
        Description = "Create new image",
        OperationId = "CreateImage",
        Tags = new[] { "Images" })]
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
    [SwaggerOperation(
        Summary = "Delete image by id",
        Description = "Delete existing image by id",
        OperationId = "DeleteImageById",
        Tags = new[] { "Images" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _imageService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var imageResource = _mapper.Map<Images, ImagesResource>(result.Resource);
        return Ok(imageResource);
    }

}