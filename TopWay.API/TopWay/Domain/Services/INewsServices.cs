using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface INewsServices
{
    Task<IEnumerable<News>> ListAsync();
    Task<IEnumerable<News>> FindByNewsIdAsync(int newsId);
    Task<News> FindByIdAsync(int id);
    Task<NewsResponse> SaveAsync(News news, int climbingGymId);
    Task<NewsResponse> UpdateAsync(News news, int climbingGymId);
    Task<NewsResponse> DeleteAsync(int newsId);
}