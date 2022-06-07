using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionGymRankingRepository
{
    Task<IEnumerable<CompetitionGymRankings>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionGymRankings>> FindByCompetitionGymIdAsync(int competitionGymId);
    Task<CompetitionGymRankings> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionGymRankings> FindByIdAsync(int id);
    Task AddAsync(CompetitionGymRankings competitionGymRankings);
    void Delete(CompetitionGymRankings competitionGymRankings);
}