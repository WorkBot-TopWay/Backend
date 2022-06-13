using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Services;

public class NewsService: INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly IClimbingGymRepository _climbingGymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NewsService(INewsRepository newsRepository, IClimbingGymRepository climbingGymRepository, IUnitOfWork unitOfWork)
    {
        _newsRepository = newsRepository;
        _climbingGymRepository = climbingGymRepository;
        _unitOfWork = unitOfWork;
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
        if (existingClimbingGym == null)
        {
            return new NewsResponse("Climbing gym not found.");
        }
        news.ClimbingGyms = existingClimbingGym;
        news.ClimbingGymsId = existingClimbingGym.Id;
        news.Date = DateTime.Now;
        try
        {
            await _newsRepository.AddAsync(news);
            await _unitOfWork.CompleteAsync();
            return new NewsResponse(news);
        }
        catch (Exception ex)
        {
            return new NewsResponse($"An error occurred when saving the news: {ex.Message}");
        }
        
    }

    public async Task<NewsResponse> Update(News news, int newsId)
    {
        var existingNews = await _newsRepository.FindByIdAsync(newsId);
        if (existingNews == null)
        {
            return new NewsResponse("News not found.");
        }
        existingNews.Title = news.Title;
        existingNews.Description = news.Description;
        existingNews.UrlImage = news.UrlImage;
        existingNews.Date = DateTime.Now;
        try
        {
            _newsRepository.Update(existingNews);
            await _unitOfWork.CompleteAsync();
            return new NewsResponse(existingNews);
        }
        catch (Exception ex)
        {
            return new NewsResponse($"An error occurred when updating the news: {ex.Message}");
        }
    }

    public async Task<NewsResponse> Delete(int newsId)
    {
        var existingNews = await _newsRepository.FindByIdAsync(newsId);
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
            return new NewsResponse($"An error occurred when deleting the news: {ex.Message}");
        }
    }
}