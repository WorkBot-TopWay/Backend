using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface INewsRepository
{
    Task<IEnumerable<News>> ListAsync();
    Task<IEnumerable<News>> FindByClimbingGymIdAsync(int climbingGymId);
    Task AddAsync(News news);
    Task<News> FindByIdAsync(int id);
    void Update(News news);
    void Delete(News news);
}