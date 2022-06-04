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

    public async Task<IEnumerable<CompetitionGym>> ListAsync()
    {
        return await _context.CompetitionGyms.ToListAsync();
    }

    public async Task<IEnumerable<CompetitionGym>> FindByClimbingGymIdAsync(int climbingGymId)
    {
        return await _context.CompetitionGyms
            .Include(c => c.ClimbingGym)
            .Where(c => c.ClimberGymId == climbingGymId)
            .ToListAsync();
    }

    public async Task<CompetitionGym> FindByIdAsync(int id)
    {
        return (await _context.CompetitionGyms
            .Include(c => c.ClimbingGym)
            .FirstOrDefaultAsync(c => c.Id == id))!;
    }

    public async Task AddAsync(CompetitionGym competitionGym)
    {
        await _context.CompetitionGyms.AddAsync(competitionGym);
    }

    public void Update(CompetitionGym competitionGym)
    {
        _context.CompetitionGyms.Update(competitionGym);
    }

    public void Delete(CompetitionGym competitionGym)
    {
        _context.CompetitionGyms.Remove(competitionGym);
    }
}