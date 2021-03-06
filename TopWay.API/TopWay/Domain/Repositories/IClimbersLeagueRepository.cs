using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IClimbersLeagueRepository
{
    Task<IEnumerable<ClimberLeagues>> GetAll();
    Task<IEnumerable<Scaler>> FindScalersByLeagueId(int leagueId);
    Task<IEnumerable<League>> FindLeaguesByClimbingGymIdAndScalerId(int climbingGymId, int scalerId);
    Task<ClimberLeagues> FindByClimbingGymIdAndScalerIdAndLeagueId(int climbingGymId, int scalerId, int leagueId);
    Task<ClimberLeagues> FindByIdAsync(int id);
    Task AddAsync(ClimberLeagues climberLeagues);
    void Delete(ClimberLeagues climberLeagues);
}