using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ImagesRepository: BaseRepository,IImagesRepository
{
    public ImagesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Images>> ListAsync()
    {
        return await _context.Images.ToListAsync();
    }

    public async Task<IEnumerable<Images>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.Images.Where(i => i.ClimbingGymId == climbingGymId).ToListAsync();
    }

    public async Task AddAsync(Images images)
    {
        await _context.Images.AddAsync(images);
    }

    public async Task<Images> FindByIdAsync(int id)
    {
        return (await _context.Images.FindAsync(id))!;
    }

    public  void Delete(Images images)
    {
        _context.Images.Remove(images);
    }
}