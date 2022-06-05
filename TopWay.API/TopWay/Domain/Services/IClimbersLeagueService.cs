using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IClimbersLeagueService
{
    Task<IEnumerable<ClimbersLeague>> GetAll();
    Task<IEnumerable<Scaler>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId);
    Task<ClimbersLeague> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId);
    Task<ClimbersLeague> FindByIdAsync(int id);
    Task<ClimbersLeagueResponse> AddAsync(ClimbersLeague climbersLeague,int climbingGymId, int scalerId, int leagueId);
    Task<ClimbersLeagueResponse> Delete(int climbingGymId, int scalerId, int leagueId);
}