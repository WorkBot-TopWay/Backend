using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionLeagueService
{
    Task<IEnumerable<CompetitionLeague>> ListAsync();
    Task<IEnumerable<CompetitionLeague>> FindByLeagueIdAsync(int leagueId);
    Task<CompetitionLeague> FindByIdAsync(int id);
    Task<CompetitionLeagueResponse> AddAsync(CompetitionLeague competitionLeague, int leagueId);
    Task<CompetitionLeagueResponse> Update(CompetitionLeague competitionLeague, int id);
    Task<CompetitionLeagueResponse> Delete(int id);   
}