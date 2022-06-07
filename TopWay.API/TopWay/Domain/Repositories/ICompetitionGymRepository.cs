using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionGymRepository
{
    Task<IEnumerable<CompetitionGyms>> ListAsync();
    Task<IEnumerable<CompetitionGyms>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<CompetitionGyms> FindByIdAsync(int id);
    Task AddAsync(CompetitionGyms competitionGyms);
    void Update(CompetitionGyms competitionGyms);
    void Delete(CompetitionGyms competitionGyms);
}