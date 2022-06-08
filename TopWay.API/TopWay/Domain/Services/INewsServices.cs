using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface INewsServices
{
    Task<IEnumerable<News>> ListAsync();
    Task<IEnumerable<News>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<News> FindByIdAsync(int id);
    Task<NewsResponse> AddAsync(News news, int climbingGymId);
    Task<NewsResponse> Update(News news, int climbingGymId);
    Task<NewsResponse> Delete(int climbingGymId);
}