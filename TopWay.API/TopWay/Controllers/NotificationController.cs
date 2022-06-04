using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Controllers;

[Route("api/v1/[controller]")]
public class NotificationController :ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    
    public NotificationController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }
    
    [HttpGet("findByScalerId/{scalerId}")]
    public async Task<IEnumerable<NotificationResource>> GetAllByScalerId(int scalerId)
    {
        var notifications = await _notificationService.FindByScalerIdAsync(scalerId);
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
    
    [HttpGet("{id}")]
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