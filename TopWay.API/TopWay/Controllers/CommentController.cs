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
public class CommentController:ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var comment = await _commentService.FindByIdAsync(id);
        if (comment == null)
            return NotFound();
        var resource = _mapper.Map<Comment, CommentResource>(comment);
        return Ok(resource);
    }
    
    [HttpGet("findByClimbingGymId/{id}")]
    public async Task<IEnumerable<CommentResource>> FindByClimbingGymIdAsync(int id)
    {
        var comments = await _commentService.FindByClimbingGymIdAsync(id);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
    
    [HttpGet("FindByClimbingGymIdAndScalerId/")]
    public async Task<IActionResult> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        var comment = await _commentService.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (comment == null)
            return NotFound();
        var resource = _mapper.Map<Comment, CommentResource>(comment);
        return Ok(resource);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource, int climbingGymId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
        var result = await _commentService.AddAsync(comment, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource, int climbingGymId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
        var result = await _commentService.UpdateAsync(comment,climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId)
    {
        var result = await _commentService.Delete(climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
}