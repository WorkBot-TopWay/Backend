using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionLeagueRankingService
{
    Task<IEnumerable<CompetitionLeagueRanking>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionLeagueRanking>> FindByCompetitionLeagueIdAsync(int competitionLeagueId);
    Task<CompetitionLeagueRanking> FindByCompetitionLeagueIdAndScalerIdAsync(int competitionLeagueId, int scalerId);
    Task<CompetitionLeagueRanking> FindByIdAsync(int id);
    Task<CompetitionLeagueRankingResponse> AddAsync(CompetitionLeagueRanking competitionLeagueRanking, int competitionLeagueId, int scalerId);
    Task<CompetitionLeagueRankingResponse> Update(CompetitionLeagueRanking competitionLeagueRanking, int competitionLeagueId, int scalerId);
    Task<CompetitionLeagueRankingResponse>  Delete(int competitionLeagueId, int scalerId);
}