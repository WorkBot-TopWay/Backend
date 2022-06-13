using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class NewsRepository: BaseRepository,INewsRepository
{
    public NewsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<News>> ListAsync()
    {
        return await _context.News.ToListAsync();
    }

    public async Task<IEnumerable<News>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.News.Where(x => x.ClimbingGymsId == climbingGymId).ToListAsync();
    }

    public async Task AddAsync(News news)
    {
        await _context.News.AddAsync(news);
    }

    public async Task<News> FindByIdAsync(int id)
    {
        return (await _context.News.FindAsync(id))!;
    }

    public void Update(News news)
    {
        _context.News.Update(news);
    }

    public void Delete(News news)
    {
        _context.News.Remove(news);
    }
}