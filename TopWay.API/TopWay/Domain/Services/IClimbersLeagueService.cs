using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IClimbersLeagueService
{
    Task<IEnumerable<ClimberLeagues>> GetAll();
    Task<IEnumerable<Scaler>> FindScalersByLeagueAndClimbingGymId(int leagueId, int climbingGymId);
    Task<IEnumerable<League>> FindLeaguesByClimbingGymIdAndScalerId(int climbingGymId, int scalerId);
    Task<ClimberLeagues> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId);
    Task<ClimberLeagues> FindByIdAsync(int id);
    Task<ClimbersLeagueResponse> AddAsync(ClimberLeagues climberLeagues,int climbingGymId, int scalerId, int leagueId);
    Task<ClimbersLeagueResponse> Delete(int climbingGymId, int scalerId, int leagueId);
}