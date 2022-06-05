using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ILeagueRepository
{
    Task<IEnumerable<League>> GetAll();
    Task<IEnumerable<League>> FindByClimbingGymId(int climbingGymId);
    
    Task<League> FindByClimbingGymIdAndScalarId(int climbingGymId, int scalarId);
    Task<League> GetById(int id);
    Task AddAsync(League league);
    void Update(League league);
    void Delete(League league);
}