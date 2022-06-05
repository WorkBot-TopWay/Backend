using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionGymRankingService
{
    Task<IEnumerable<CompetitionGymRanking>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<CompetitionGymRanking> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionGymRanking> FindByIdAsync(int id);
    Task<CompetitionGymRankingResponse> AddAsync(CompetitionGymRanking competitionGymRanking,int competitionId, int scalerId);
    Task<CompetitionGymRankingResponse> Delete(int competitionId, int scalerId);
}
