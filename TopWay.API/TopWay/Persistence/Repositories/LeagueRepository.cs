using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class LeagueRepository: BaseRepository, ILeagueRepository
{
    public LeagueRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<League>> GetAll()
    {
        return await _context.Leagues.ToListAsync();
    }

    public async Task<IEnumerable<League>> FindByClimbingGymId(int climbingGymId)
    {
        return await _context.Leagues.Where(l => l.ClimbingGymId == climbingGymId).ToListAsync();
    }

    public async Task<League> FindByClimbingGymIdAndScalarId(int climbingGymId, int scalarId)
    {
        return (await _context.Leagues.FirstOrDefaultAsync(l => l.ClimbingGymId == climbingGymId && l.ScalerId == scalarId))!;
    }

    public async Task<League> GetById(int id)
    {
        return (await _context.Leagues.FindAsync(id))!;
    }

    public async Task AddAsync(League league)
    {
        await _context.Leagues.AddAsync(league);
    }


    public void Update(League league)
    {
        _context.Leagues.Update(league);
    }

    public void Delete(League league)
    {
        _context.Leagues.Remove(league);
    }
}