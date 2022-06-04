using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionGymService
{
    Task<IEnumerable<CompetitionGym>> ListAsync();
    Task<IEnumerable<CompetitionGym>> FindByClimbingGymIdAsync(int climbingGymId);
    Task<CompetitionGym> FindByIdAsync(int id);
    Task<CompetitionGymResponse> SaveAsync(CompetitionGym competitionGym, int climbingGymId);
    Task<CompetitionGymResponse> UpdateAsync(int id, CompetitionGym competitionGym);
    Task<CompetitionGymResponse> DeleteAsync(int id);
}