using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAll();
    Task<IEnumerable<Request>> FindByScalerId(int scalerId);
    
    Task<Request> FindLeagueIdAndScapeId(int leagueId, int scapeId);
    Task<IEnumerable<Scaler>> FindRequestScalerByLeagueId(int leagueId);
    Task<Request> FindByIdAsync(int id);
    Task AddAsync(Request request);
    void Delete(Request request);
}