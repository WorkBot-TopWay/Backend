using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionGymRepository : BaseRepository, ICompetitionGymRepository
{
    public CompetitionGymRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionGyms>> ListAsync()
    {
        return await _context.CompetitionGyms.ToListAsync();
    }

    public async Task<IEnumerable<CompetitionGyms>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.CompetitionGyms
            .Include(c => c.ClimbingGyms)
            .Where(c => c.ClimberGymId == climbingGymId)
            .ToListAsync();
    }

    public async Task<CompetitionGyms> FindByIdAsync(int id)
    {
        return (await _context.CompetitionGyms
            .Include(c => c.ClimbingGyms)
            .FirstOrDefaultAsync(c => c.Id == id))!;
    }

    public async Task AddAsync(CompetitionGyms competitionGyms)
    {
        await _context.CompetitionGyms.AddAsync(competitionGyms);
    }

    public void Update(CompetitionGyms competitionGyms)
    {
        _context.CompetitionGyms.Update(competitionGyms);
    }

    public void Delete(CompetitionGyms competitionGyms)
    {
        _context.CompetitionGyms.Remove(competitionGyms);
    }
}