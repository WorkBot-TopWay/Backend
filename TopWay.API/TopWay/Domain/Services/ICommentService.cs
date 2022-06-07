using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comments>> ListAsync();
    Task<IEnumerable<Comments>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<Comments> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
    Task<Comments> FindByIdAsync(int id);
    Task<CommentResponse> AddAsync(Comments comments,int climbingGymId, int scalerId);
    Task<CommentResponse> UpdateAsync(Comments comments,int climbingGymId, int scalerId);
    Task<CommentResponse> Delete(int climbingGymId, int scalerId);
}