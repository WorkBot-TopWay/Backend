using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionGymRankingService
{
    Task<IEnumerable<CompetitionGymRankings>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionGymRankings>> FindByCompetitionGymIdAsync(int competitionGymId);
    Task<CompetitionGymRankings> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionGymRankings> FindByIdAsync(int id);
    Task<CompetitionGymRankingResponse> AddAsync(CompetitionGymRankings competitionGymRankings,int competitionId, int scalerId);
    Task<CompetitionGymRankingResponse> UpdateAsync(CompetitionGymRankings competitionGymRankings, int competitionId, int scalerId);
    Task<CompetitionGymRankingResponse> Delete(int competitionId, int scalerId);
}
