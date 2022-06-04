using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IImagesRepository
{
    Task<IEnumerable<Images>> ListAsync();
    Task<IEnumerable<Images>> FindByClimbingGymIdAsync(int climbingGymId);
    Task AddAsync(Images images);
    Task<Images> FindByIdAsync(int id);
    void Delete(Images images);
}