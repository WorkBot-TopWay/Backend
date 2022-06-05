using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionGymRankingRepository: BaseRepository, ICompetitionGymRankingRepository
{
    public CompetitionGymRankingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionGymRanking>> ListAsync()
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

    public async Task<IEnumerable<CompetitionGymRanking>> FindByCompetitionGymIdAsync(int competitionGymId)
    {
        return await _context.CompetitionGymRankings
            .Where(c => c.CompetitionGymId == competitionGymId)
            .ToListAsync();
    }

    public async Task<CompetitionGymRanking> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        return (await _context.CompetitionGymRankings
            .Where(c => c.CompetitionGymId == competitionId && c.ScalerId == scalerId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CompetitionGymRanking> FindByIdAsync(int id)
    {
        return (await _context.CompetitionGymRankings.FindAsync(id))!;
    }

    public async Task AddAsync(CompetitionGymRanking competitionGymRanking)
    {
        await _context.CompetitionGymRankings.AddAsync(competitionGymRanking);
    }

    public void Delete(CompetitionGymRanking competitionGymRanking)
    {
        _context.CompetitionGymRankings.Remove(competitionGymRanking);
    }
}