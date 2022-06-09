using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IScalerService
{
    Task<IEnumerable<Scaler>> ListAsync();
    Task<Scaler> FindByIdAsync(int id);
    Task<Scaler> FindByIdEmailAndPasswordAsync(string email, string password);
    Task<ScalerResponse> SaveAsync(Scaler scaler);
    Task<ScalerResponse> UpdateAsync(int id, Scaler scaler);
    Task<ScalerResponse> DeleteAsync(int id);
}