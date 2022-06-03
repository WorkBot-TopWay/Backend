using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ClimbingGymRepository : BaseRepository, IClimbingGymRepository
{
    public ClimbingGymRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ClimbingGym>> ListAsync()
    {
        return await _context.ClimbingGyms.ToListAsync();
    }

    public async Task AddAsync(ClimbingGym climbingGym)
    {
        await _context.ClimbingGyms.AddAsync(climbingGym);
    }

    public async Task<ClimbingGym> FindByIdAsync(int id)
    {
        return (await _context.ClimbingGyms.FindAsync(id))!;
    }

    public void Update(ClimbingGym climbingGym)
    {
        _context.ClimbingGyms.Update(climbingGym);
    }

    public void Delete(ClimbingGym climbingGym)
    {
        _context.ClimbingGyms.Remove(climbingGym);
    }
}