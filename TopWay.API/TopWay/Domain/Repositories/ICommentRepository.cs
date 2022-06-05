using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<IEnumerable<Comment>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<Comment> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
    Task<Comment> FindByIdAsync(int id);
    Task AddAsync(Comment comment);
    void UpdateAsync(Comment comment);
    void Delete(Comment comment);
}