using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionLeagueRepository: BaseRepository, ICompetitionLeagueRepository
{
    public CompetitionLeagueRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionLeague>> ListAsync()
    {
        return await _context.CompetitionLeagues.ToListAsync();
    }

    public async Task<IEnumerable<CompetitionLeague>> FindByLeagueIdAsync(int leagueId)
    {
        return await _context.CompetitionLeagues.Where(x => x.LeagueId == leagueId).ToListAsync();
    }
    
    public async Task<CompetitionLeague> FindByIdAsync(int id)
    {
        return (await _context.CompetitionLeagues.FindAsync(id))!;
    }

    public async Task AddAsync(CompetitionLeague competitionLeague)
    {
        await _context.CompetitionLeagues.AddAsync(competitionLeague);
    }

    public void Update(CompetitionLeague competitionLeague)
    {
        _context.CompetitionLeagues.Update(competitionLeague);
    }

    public void Delete(CompetitionLeague competitionLeague)
    {
        _context.CompetitionLeagues.Remove(competitionLeague);
    }
}