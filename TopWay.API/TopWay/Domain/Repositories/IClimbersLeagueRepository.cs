using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IClimbersLeagueRepository
{
    Task<IEnumerable<ClimbersLeague>> GetAll();
    Task<IEnumerable<Scaler>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId);
    Task<ClimbersLeague> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId);
    Task<ClimbersLeague> FindByIdAsync(int id);
    Task AddAsync(ClimbersLeague climbersLeague);
    void Delete(ClimbersLeague climbersLeague);
}