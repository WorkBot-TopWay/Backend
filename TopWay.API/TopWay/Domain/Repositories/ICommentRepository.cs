using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comments>> ListAsync();
    Task<IEnumerable<Comments>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<Comments> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
    Task<Comments> FindByIdAsync(int id);
    Task AddAsync(Comments comments);
    void UpdateAsync(Comments comments);
    void Delete(Comments comments);
}