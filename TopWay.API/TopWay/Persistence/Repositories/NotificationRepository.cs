using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class NotificationRepository: BaseRepository, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> FindAllAsync()
    {
        return await _context.Notifications.ToListAsync();
    }

    public async Task<IEnumerable<Notification>> FindByScalerIdAsync(int scalerId)
    {
        return await _context.Notifications
            .Include(p => p.Scaler)
            .Where(p => p.ScalerId == scalerId)
            .ToListAsync();
    }

    public async Task<Notification> FindByIdAsync(int id)
    {
        return (await _context.Notifications.FindAsync(id))!;
    }

    public async Task AddAsync(Notification notification)
    {
         await _context.Notifications.AddAsync(notification);
    }

    public void Delete(Notification notification)
    {
       _context.Notifications.Remove(notification);
    }
}