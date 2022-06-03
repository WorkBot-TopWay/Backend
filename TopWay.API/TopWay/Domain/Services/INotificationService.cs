using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> FindByScalerIdAsync(int scalerId);
    
    Task<Notification> FindByIdAsync(int id);
    Task<NotificationResponse> SaveAsync(Notification notification, int scalerId);
    Task<NotificationResponse> DeleteAsync(int id);
}