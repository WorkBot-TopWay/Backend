using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionGymRankingRepository
{
    Task<IEnumerable<CompetitionGymRanking>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<CompetitionGymRanking> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionGymRanking> FindByIdAsync(int id);
    Task AddAsync(CompetitionGymRanking competitionGymRanking);
    void Delete(CompetitionGymRanking competitionGymRanking);
}