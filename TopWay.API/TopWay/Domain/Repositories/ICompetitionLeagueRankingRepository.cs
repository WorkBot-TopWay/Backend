using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionLeagueRankingRepository
{
    Task<IEnumerable<CompetitionLeagueRanking>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionLeagueRanking>> FindByCompetitionLeagueIdAsync(int competitionLeagueId);
    Task<CompetitionLeagueRanking> FindByCompetitionLeagueIdAndScalerIdAsync(int competitionLeagueId, int scalerId);
    Task<CompetitionLeagueRanking> FindByIdAsync(int id);
    Task AddAsync(CompetitionLeagueRanking competitionLeagueRanking);
    void Delete(CompetitionLeagueRanking competitionLeagueRanking);
}