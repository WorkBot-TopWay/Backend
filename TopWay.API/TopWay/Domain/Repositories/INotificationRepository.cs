using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> FindByScalerIdAsync(int scalerId);
    
    Task<Notification> FindByIdAsync(int id);
    Task AddAsync(Notification notification);
    void Delete(Notification notification);
}