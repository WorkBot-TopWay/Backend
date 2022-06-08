using System.Diagnostics.CodeAnalysis;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class NewsService : INewsServices
{
    private readonly INewsRepository _newsRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork, IClimbingGymRepository climbingGymRepository)
    {
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
        _climbingGymRepository = climbingGymRepository;
    }
    
    public async Task<IEnumerable<News>> ListAsync()
    {
        return await _newsRepository.ListAsync();
    }

    public async Task<IEnumerable<News>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _newsRepository.FindByClimbingGymIdAsync(climbingGymId);
    }

    public async Task<News> FindByIdAsync(int id)
    {
        return await _newsRepository.FindByIdAsync(id);
    }

    public async Task<NewsResponse> AddAsync(News news, int climbingGymId)
    {
        var existingClimbingGym = await _climbingGymRepository.FindByIdAsync(climbingGymId);
        var existingNews = await _newsRepository.FindByClimbingGymIdAsync(climbingGymId);

        if (existingNews != null)
        {
            return new NewsResponse("That News already exists");
        }
        
        if(existingClimbingGym == null)
        {
            return new NewsResponse("Climbing gym not found.");
        }
        
        news.Date = DateTime.Now;
        news.ClimbingGym = existingClimbingGym;
        news.ClimbingGymId = climbingGymId;

        try
        {
            news.ClimbingGym = existingClimbingGym;
            await _newsRepository.AddAsync(news);
            await _unitOfWork.CompleteAsync();

            return new NewsResponse(news);
        }
        catch (Exception ex)
        {
            return new NewsResponse($"An error occurred when saving the comment: {ex.Message}");
        }
    }

    public async Task<NewsResponse> Update(News news, int id)
    {
        var existingNews = await _newsRepository.FindByIdAsync(id);
        
        if (existingNews == null)
        {
            return new NewsResponse("News not found.");
        }
        
        existingNews.Date = DateTime.Now;

        try
        {
            _newsRepository.UpdateAsync(existingNews);
            await _unitOfWork.CompleteAsync();

            return new NewsResponse(existingNews);
        }
        catch (Exception ex)
        {
            return new NewsResponse($"An error occurred when updating the comment: {ex.Message}");
        }
    }

    public async Task<NewsResponse> Delete(int id)
    {
        var existingNews = await _newsRepository.FindByIdAsync(id);
        if (existingNews == null)
        {
            return new NewsResponse("News not found.");
        }
        try
        {
            _newsRepository.Delete(existingNews);
            await _unitOfWork.CompleteAsync();

            return new NewsResponse(existingNews);
        }
        catch (Exception ex)
        {
            return new NewsResponse($"An error occurred when deleting the comment: {ex.Message}");
        }
    }
}