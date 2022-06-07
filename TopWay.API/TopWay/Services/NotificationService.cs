using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IScalerRepository _scalerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IScalerRepository scalerRepository)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _scalerRepository = scalerRepository;
    }

    public async Task<IEnumerable<Notification>> FindAllAsync()
    {
        return await _notificationRepository.FindAllAsync();
    }

    public async Task<IEnumerable<Notification>> FindByScalerIdAsync(int scalerId)
    {
       var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
       if (existingScaler == null)
       {
           return null;
       }

       return await _notificationRepository.FindByScalerIdAsync(scalerId);
    }

    public async Task<Notification> FindByIdAsync(int id)
    {
        return await _notificationRepository.FindByIdAsync(id);
    }

    public async Task<NotificationResponse> SaveAsync(Notification notification, int scalerId)
    {
        var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
        if (existingScaler == null)
        {
            return new NotificationResponse("Scaler not found.");
        }
        notification.ScalerId = existingScaler.Id;
        notification.Scaler = existingScaler;
        try
        {
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            return new NotificationResponse(notification);
        }
        catch (Exception ex)
        {
            return new NotificationResponse($"An error occurred when saving the notification: {ex.Message}");
        }
    }

    public async Task<NotificationResponse> DeleteAsync(int id)
    {
        var existingScaler = await _notificationRepository.FindByIdAsync(id);
        if (existingScaler == null)
        {
            return new NotificationResponse("Notification not found.");
        }
        try
        {
            _notificationRepository.Delete(existingScaler);
            await _unitOfWork.CompleteAsync();

            return new NotificationResponse(existingScaler);
        }
        catch (Exception ex)
        {
            return new NotificationResponse($"An error occurred when deleting the notification: {ex.Message}");
        }
    }
}