using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<IEnumerable<Comment>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<Comment> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
    Task<Comment> FindByIdAsync(int id);
    Task<CommentResponse> AddAsync(Comment comment,int climbingGymId, int scalerId);
    Task<CommentResponse> UpdateAsync(Comment comment,int climbingGymId, int scalerId);
    Task<CommentResponse> Delete(int climbingGymId, int scalerId);
}