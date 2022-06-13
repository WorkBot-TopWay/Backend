using Microsoft.EntityFrameworkCore;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionGymRankingRepository: BaseRepository, ICompetitionGymRankingRepository
{
    public CompetitionGymRankingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionGymRankings>> ListAsync()
    {
        return await _context.CompetitionGymRankings.ToListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
       return await _context.Scalers
           .Include(s => s.CompetitionGymRankings)
           .Where(s => s.CompetitionGymRankings.Any(c => c.CompetitionGymId == competitionId))
           .ToListAsync();
    }

    public async Task<IEnumerable<CompetitionGymRankings>> FindByCompetitionGymIdAsync(int competitionGymId)
    {
        return await _context.CompetitionGymRankings
            .Where(c => c.CompetitionGymId == competitionGymId)
            .ToListAsync();
    }

    public async Task<CompetitionGymRankings> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        return (await _context.CompetitionGymRankings
            .Where(c => c.CompetitionGymId == competitionId && c.ScalerId == scalerId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CompetitionGymRankings> FindByIdAsync(int id)
    {
        return (await _context.CompetitionGymRankings.FindAsync(id))!;
    }

    public async Task AddAsync(CompetitionGymRankings competitionGymRankings)
    {
        await _context.CompetitionGymRankings.AddAsync(competitionGymRankings);
    }

    public void Delete(CompetitionGymRankings competitionGymRankings)
    {
        _context.CompetitionGymRankings.Remove(competitionGymRankings);
    }
}