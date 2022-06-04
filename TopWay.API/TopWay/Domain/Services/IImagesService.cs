using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IImagesService
{
    Task<IEnumerable<Images>> FindAllAsync();
    Task<IEnumerable<Images>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<Images> FindByIdAsync(int id);
    Task<ImagesResponse> SaveAsync(Images images, int climbingGymId);
    Task<ImagesResponse> DeleteAsync(int id);
}