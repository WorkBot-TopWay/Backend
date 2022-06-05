using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class CommentService: ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScalerRepository _scalerRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IScalerRepository scalerRepository, IClimbingGymRepository climbingGymRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _scalerRepository = scalerRepository;
        _climbingGymRepository = climbingGymRepository;
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }

    public async Task<IEnumerable<Comment>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _commentRepository.FindByClimbingGymIdAsync(climbingGymId);
    }

    public async Task<Comment> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
    }

    public async Task<Comment> FindByIdAsync(int id)
    {
        return await _commentRepository.FindByIdAsync(id);
    }

    public async Task<CommentResponse> AddAsync(Comment comment, int climbingGymId, int scalerId)
    {
       var existingScaler = await _scalerRepository.FindByIdAsync(scalerId);
       var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
       var existingComment = await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
       if (existingComment != null)
       {
           return new CommentResponse("Comment already exists");
       }
       if (existingScaler == null)
       {
           return new CommentResponse("Scaler not found.");
       }
       if(existingClimbingGym == null)
       {
           return new CommentResponse("Climbing gym not found.");
       }
       comment.Date = DateTime.Now;
       comment.Scaler = existingScaler;
       comment.ClimbingGym = existingClimbingGym;
       comment.ScalerId = scalerId;
       comment.ClimbingGymId = climbingGymId;
    
       try
       {
           comment.ClimbingGym = existingClimbingGym;
           comment.Scaler = existingScaler;
           await _commentRepository.AddAsync(comment);
           await _unitOfWork.CompleteAsync();

           return new CommentResponse(comment);
       }
       catch (Exception ex)
       {
           return new CommentResponse($"An error occurred when saving the comment: {ex.Message}");
       }
        
    }

    public async Task<CommentResponse> UpdateAsync(Comment comment, int climbingGymId, int scalerId)
    {
        var existingComment =await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingComment == null)
        {
            return new CommentResponse("Comment not found.");
        }
        existingComment.Date = DateTime.Now;
        existingComment.Description = comment.Description;
        existingComment.Score = comment.Score;
        try
        {
            _commentRepository.UpdateAsync(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception ex)
        {
            return new CommentResponse($"An error occurred when updating the comment: {ex.Message}");
        }
    }


    public async Task<CommentResponse> Delete(int climbingGymId, int scalerId)
    {
        var existingComment =await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingComment == null)
        {
            return new CommentResponse("Comment not found.");
        }
        try
        {
            _commentRepository.Delete(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception ex)
        {
            return new CommentResponse($"An error occurred when deleting the comment: {ex.Message}");
        }
    }
}