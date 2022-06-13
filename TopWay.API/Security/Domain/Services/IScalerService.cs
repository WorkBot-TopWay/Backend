using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.Security.Domain.Services;

public interface IScalerService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
    Task<IEnumerable<Scaler>> ListAsync();
    Task<Scaler> FindByIdAsync(int id);
    Task RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}