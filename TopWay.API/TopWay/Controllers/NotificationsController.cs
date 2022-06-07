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
[SwaggerTag(" Create, Read and Delete a Notifications")]
public class NotificationsController :ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    
    public NotificationsController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all notifications or find by scaler id", 
        Description = "Get all notifications or find by scaler id",
        OperationId = "GetNotifications",
        Tags = new[] { "Notifications" })]
    public async  Task<IActionResult> GetAllByScalerId([FromQuery]string?  scalerId=null)
    {
        if (scalerId != null)
        {
            int id = int.Parse(scalerId);
            var notifications = await _notificationService.FindByScalerIdAsync(id);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return Ok(resources);
        }

        var notifications1 = await _notificationService.FindAllAsync();
        var resources1 = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications1);
        return Ok(resources1);

    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get notification by id", 
        Description = "Get existing notification by id",
        OperationId = "GetNotification",
        Tags = new[] { "Notifications" })]
    public async Task<IActionResult> GetById(int id)
    {
        var notification = await _notificationService.FindByIdAsync(id);
        if (notification == null)
        {
            return NotFound();
        }
        var resource = _mapper.Map<Notification, NotificationResource>(notification);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerResponse(200, "The operation was success", typeof(CategoriesResource))]
    [SwaggerResponse(400, "The operation was unsuccess")]
    [SwaggerOperation(
        Summary = "Create new notification", 
        Description = "Create new notification",
        OperationId = "CreateNotification",
        Tags = new[] { "Notifications" })]
    public async Task<IActionResult> PostAsync(int scalerId,[FromBody] SaveNotificationResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetErrorMessages());
        }
        var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);
        var result = await _notificationService.SaveAsync(notification,scalerId);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
        return Ok(notificationResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete notification by id", 
        Description = "Delete existing notification by id",
        OperationId = "DeleteNotification",
        Tags = new[] { "Notifications" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _notificationService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
        return Ok(notificationResource);
    }
    
}