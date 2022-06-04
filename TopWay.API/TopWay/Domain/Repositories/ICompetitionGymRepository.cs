using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionGymRepository
{
    Task<IEnumerable<CompetitionGym>> ListAsync();
    Task<IEnumerable<CompetitionGym>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<CompetitionGym> FindByIdAsync(int id);
    Task AddAsync(CompetitionGym competitionGym);
    void Update(CompetitionGym competitionGym);
    void Delete(CompetitionGym competitionGym);
}