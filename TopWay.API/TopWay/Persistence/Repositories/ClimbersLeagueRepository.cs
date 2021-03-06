using Microsoft.EntityFrameworkCore;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ClimbersLeagueRepository: BaseRepository,IClimbersLeagueRepository
{
    public ClimbersLeagueRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ClimberLeagues>> GetAll()
    {
        return await _context.ClimbersLeagues.ToListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalersByLeagueId(int leagueId)
    {
        return await _context.Scalers
            .Include(p=>p.ClimbersLeagues)
            .Where(p=>p.ClimbersLeagues.Any(c=>c.LeagueId==leagueId))
            .ToListAsync();

    }

    public async Task<IEnumerable<League>> FindLeaguesByClimbingGymIdAndScalerId(int climbingGymId, int scalerId)
    {
        return await _context.Leagues
            .Include(p=>p.ClimbersLeagues)
            .Where(p=>p.ClimbersLeagues.Any(l=>l.ClimbingGymId==climbingGymId && l.ScalerId==scalerId))
            .ToListAsync();
    }

    public async Task<ClimberLeagues> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId)
    {
        return (await _context.ClimbersLeagues
            .Include(p=>p.ClimbingGyms)
            .Include(p=>p.Scaler)
            .Include(p=>p.League)
            .FirstOrDefaultAsync(p=>p.ClimbingGymId==climbingGymId && p.ScalerId==scalerId && p.LeagueId==leagueId))!;
    }

    public async Task<ClimberLeagues> FindByIdAsync(int id)
    {
       return (await _context.ClimbersLeagues.FindAsync(id))!;
    }

    public async Task AddAsync(ClimberLeagues climberLeagues)
    {
        await _context.ClimbersLeagues.AddAsync(climberLeagues);
    }

    public void Delete(ClimberLeagues climberLeagues)
    {
        _context.ClimbersLeagues.Remove(climberLeagues);
    }
}