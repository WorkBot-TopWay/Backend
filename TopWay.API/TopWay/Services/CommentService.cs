using TopWay.API.Security.Domain.Repositories;
using TopWay.API.Shared.Domain.Repositories;
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

    public async Task<IEnumerable<Comments>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }

    public async Task<IEnumerable<Comments>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _commentRepository.FindByClimbingGymIdAsync(climbingGymId);
    }

    public async Task<Comments> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
    }

    public async Task<Comments> FindByIdAsync(int id)
    {
        return await _commentRepository.FindByIdAsync(id);
    }

    public async Task<CommentResponse> AddAsync(Comments comments, int climbingGymId, int scalerId)
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
       comments.Date = DateTime.Now;
       comments.Scaler = existingScaler;
       comments.ClimbingGyms = existingClimbingGym;
       comments.ScalerId = scalerId;
       comments.ClimbingGymId = climbingGymId;
    
       try
       {
           comments.ClimbingGyms = existingClimbingGym;
           comments.Scaler = existingScaler;
           await _commentRepository.AddAsync(comments);
           await _unitOfWork.CompleteAsync();

           return new CommentResponse(comments);
       }
       catch (Exception ex)
       {
           return new CommentResponse($"An error occurred when saving the comment: {ex.Message}");
       }
        
    }

    public async Task<CommentResponse> UpdateAsync(Comments comments, int climbingGymId, int scalerId)
    {
        var existingComment =await _commentRepository.FindByClimbingGymIdAndScalerIdAsync(climbingGymId, scalerId);
        if (existingComment == null)
        {
            return new CommentResponse("Comment not found.");
        }
        existingComment.Date = DateTime.Now;
        existingComment.Description = comments.Description;
        existingComment.Score = comments.Score;
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