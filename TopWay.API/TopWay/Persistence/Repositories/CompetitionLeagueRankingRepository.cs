using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionLeagueRankingRepository: BaseRepository, ICompetitionLeagueRankingRepository
{
    public CompetitionLeagueRankingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionLeagueRanking>> ListAsync()
    {
        return await _context.CompetitionLeagueRankings.ToListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        return await _context.Scalers
            .Include(p=>p.CompetitionLeagueRankings)
            .Where(p=>p.CompetitionLeagueRankings.Any(c=>c.CompetitionLeagueId == competitionId))
            .ToListAsync();
    }

    public async Task<IEnumerable<CompetitionLeagueRanking>> FindByCompetitionLeagueIdAsync(int competitionLeagueId)
    {
        return await _context.CompetitionLeagueRankings
            .Where(p=>p.CompetitionLeagueId == competitionLeagueId)
            .ToListAsync();
    }

    public async Task<CompetitionLeagueRanking> FindByCompetitionLeagueIdAndScalerIdAsync(int competitionLeagueId, int scalerId)
    {
        return (await _context.CompetitionLeagueRankings
            .Where(p=>p.CompetitionLeagueId == competitionLeagueId && p.ScalerId == scalerId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CompetitionLeagueRanking> FindByIdAsync(int id)
    {
        return (await _context.CompetitionLeagueRankings
            .Where(p=>p.Id == id)
            .FirstOrDefaultAsync())!;
    }

    public async Task AddAsync(CompetitionLeagueRanking competitionLeagueRanking)
    {
        await _context.CompetitionLeagueRankings.AddAsync(competitionLeagueRanking);
    }

    public void Delete(CompetitionLeagueRanking competitionLeagueRanking)
    {
        _context.CompetitionLeagueRankings.Remove(competitionLeagueRanking);
    }
}