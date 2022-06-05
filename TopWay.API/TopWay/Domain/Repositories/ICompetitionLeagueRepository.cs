using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionLeagueRepository
{
    Task<IEnumerable<CompetitionLeague>> ListAsync();
    Task<IEnumerable<CompetitionLeague>> FindByLeagueIdAsync(int leagueId);
    Task<CompetitionLeague> FindByIdAsync(int id);
    Task AddAsync(CompetitionLeague competitionLeague);
    void Update(CompetitionLeague competitionLeague);
    void Delete(CompetitionLeague competitionLeague);
}