using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class FavoriteRepository: BaseRepository, IFavoriteRepository
{
    public FavoriteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Favorite>> ListAsync()
    {
        return await _context.Favorites.ToListAsync();
    }

    public async Task<IEnumerable<Favorite>> FindByScalerIdAsync(int scalerId)
    {
        return await _context.Favorites
            .Where(c => c.ScalerId == scalerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ClimbingGyms>> FindClimbingGymByScalerIdAsync(int scalerId)
    {
        return await _context.ClimbingGyms
            .Include(c => c.Favorites)
            .Where(c => c.Favorites.Any(f => f.ScalerId == scalerId))
            .ToListAsync();
    }


    public async Task<Favorite> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId)
    {
        return (await _context.Favorites
            .FirstOrDefaultAsync(c=>c.ClimbingGymId == climbingGymId && c.ScalerId == scalerId))!;
    }

    public async Task<Favorite> FindByIdAsync(int id)
    {
        return (await _context.Favorites.FindAsync(id))!;
    }

    public async Task AddAsync(Favorite favorite)
    {
        await _context.Favorites.AddAsync(favorite);
    }

    public void Update(Favorite favorite)
    {
        _context.Favorites.Update(favorite);
    }

    public void Delete(Favorite favorite)
    {
        _context.Favorites.Remove(favorite);
    }
}