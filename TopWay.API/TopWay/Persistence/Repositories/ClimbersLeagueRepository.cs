using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ClimbersLeagueRepository: BaseRepository,IClimbersLeagueRepository
{
    public ClimbersLeagueRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ClimbersLeague>> GetAll()
    {
        return await _context.ClimbersLeagues.ToListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId)
    {
        return await _context.Scalers
            .Include(p=>p.ClimbersLeagues)
            .Where(p=>p.ClimbersLeagues.Any(l=>l.ClimbingGymId==climbingGymId && l.LeagueId==leagueId))
            .ToListAsync();

    }

    public async Task<ClimbersLeague> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId)
    {
        return (await _context.ClimbersLeagues
            .Include(p=>p.ClimbingGym)
            .Include(p=>p.Scaler)
            .Include(p=>p.League)
            .FirstOrDefaultAsync(p=>p.ClimbingGymId==climbingGymId && p.ScalerId==scalerId && p.LeagueId==leagueId))!;
    }

    public async Task<ClimbersLeague> FindByIdAsync(int id)
    {
       return (await _context.ClimbersLeagues.FindAsync(id))!;
    }

    public async Task AddAsync(ClimbersLeague climbersLeague)
    {
        await _context.ClimbersLeagues.AddAsync(climbersLeague);
    }

    public void Delete(ClimbersLeague climbersLeague)
    {
        _context.ClimbersLeagues.Remove(climbersLeague);
    }
}