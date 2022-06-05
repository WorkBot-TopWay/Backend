using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ILeagueService
{
    Task<IEnumerable<League>> GetAll();
    Task<IEnumerable<League>> FindByClimbingGymId(int climbingGymId);
    Task<League> GetById(int id);
    Task<LeagueResponse> Add(League league,int climbingGymId, int scaleId);
    
    Task<LeagueResponse> Update(League league,int leagueId);

    Task<LeagueResponse> UpdateNumberParticipant(int leagueId, int scaleId);
    
    Task<LeagueResponse> DeleteParticipant(int leagueId, int scaleId);
    Task<LeagueResponse> Delete(int leagueId);
}