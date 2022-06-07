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
[SwaggerTag(" Create, Read, Update and Delete a Comment")]
public class CommentsController:ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all comments or find comments by climbingGymId and userId or find comments by climbingGymId",
        Description = "Get all comments or find comments by climbingGymId and userId or find comments by climbingGymId",
        OperationId = "GetComments",
        Tags = new[] { "Comments" })]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? climbingGymId = null, [FromQuery] string? scalerId=null)
    {
        if (climbingGymId != null && scalerId != null)
        {
            int climbingGymIdInt = int.Parse(climbingGymId);
            int scalerIdInt = int.Parse(scalerId);
            var comment = await _commentService.FindByClimbingGymIdAndScalerIdAsync(climbingGymIdInt, scalerIdInt);
            if (comment == null)
                return NotFound();
            var resource = _mapper.Map<Comments, CommentResource>(comment);
            return Ok(resource);
        }
        if(climbingGymId != null)
        {
            int climbingGymIdInt = int.Parse(climbingGymId);
            var commentsA = await _commentService.FindByClimbingGymIdAsync(climbingGymIdInt);
            var resourcesA = _mapper.Map<IEnumerable<Comments>, IEnumerable<CommentResource>>(commentsA);
            return Ok(resourcesA);
        }
        
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comments>, IEnumerable<CommentResource>>(comments);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get comment by id",
        Description = "Get comment by id",
        OperationId = "GetCommentById",
        Tags = new[] { "Comments" })]
    public async Task<IActionResult> GetAsync(int id)
    {
        var comment = await _commentService.FindByIdAsync(id);
        if (comment == null)
            return NotFound();
        var resource = _mapper.Map<Comments, CommentResource>(comment);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create a comment",
        Description = "Create a new comment", 
        OperationId = "CreateComment",
        Tags = new[] { "Comments" })]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource, int climbingGymId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var comment = _mapper.Map<SaveCommentResource, Comments>(resource);
        var result = await _commentService.AddAsync(comment, climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comments, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a comment",
        Description = "Update a existing comment",
        OperationId = "UpdateComment",
        Tags = new[] { "Comments" })]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource, int climbingGymId, int scalerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var comment = _mapper.Map<SaveCommentResource, Comments>(resource);
        var result = await _commentService.UpdateAsync(comment,climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comments, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a comment",
        Description = "Delete a existing comment",
        OperationId = "DeleteComment",
        Tags = new[] { "Comments" })]
    public async Task<IActionResult> DeleteAsync(int climbingGymId, int scalerId)
    {
        var result = await _commentService.Delete(climbingGymId, scalerId);
        if (!result.Success)
            return BadRequest(result.Message);
        var commentResource = _mapper.Map<Comments, CommentResource>(result.Resource);
        return Ok(commentResource);
    }
}