using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionGymService
{
    Task<IEnumerable<CompetitionGyms>> ListAsync();
    Task<IEnumerable<CompetitionGyms>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<CompetitionGyms> FindByIdAsync(int id);
    Task<CompetitionGymResponse> SaveAsync(CompetitionGyms competitionGyms, int climbingGymId);
    Task<CompetitionGymResponse> UpdateAsync(int id, CompetitionGyms competitionGyms);
    Task<CompetitionGymResponse> DeleteAsync(int id);
}