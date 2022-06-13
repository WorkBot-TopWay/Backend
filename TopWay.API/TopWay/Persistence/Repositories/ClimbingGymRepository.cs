using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ClimbingGymRepository : BaseRepository, IClimbingGymRepository
{
    public ClimbingGymRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ClimbingGyms>> ListAsync()
    {
        return await _context.ClimbingGyms.ToListAsync();
    }

    public async Task AddAsync(ClimbingGyms climbingGyms)
    {
        await _context.ClimbingGyms.AddAsync(climbingGyms);
    }

    public async Task<ClimbingGyms> FindByIdAsync(int id)
    {
        return (await _context.ClimbingGyms.FindAsync(id))!;
    }

    public async Task<ClimbingGyms> FindByNameAsync(string name)
    {
        return (await _context.ClimbingGyms
            .Where(p=>p.Name == name)
            .FirstOrDefaultAsync())!;
    }

    public void Update(ClimbingGyms climbingGyms)
    {
        _context.ClimbingGyms.Update(climbingGyms);
    }

    public void Delete(ClimbingGyms climbingGyms)
    {
        _context.ClimbingGyms.Remove(climbingGyms);
    }
}