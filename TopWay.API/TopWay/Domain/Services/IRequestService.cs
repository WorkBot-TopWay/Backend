using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IRequestService
{
    Task<IEnumerable<Request>> GetAll();
    Task<IEnumerable<Request>> FindByScalerId(int scalerId);
    Task<Request> FindLeagueIdAndScapeId(int leagueId, int scalerId);
    Task<IEnumerable<Scaler>> FindRequestScalerByLeagueId(int leagueId);
    Task<Request> FindByIdAsync(int id);
    Task<RequestResponse> AddAsync(Request request, int leagueId, int scalerId);
    Task<RequestResponse> Delete(int leagueId, int scalerId);
}