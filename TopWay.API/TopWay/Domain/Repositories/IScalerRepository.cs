using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IScalerRepository
{
    Task<IEnumerable<Scaler>> ListAsync();
    Task AddAsync(Scaler category);
    Task<Scaler> FindByIdAsync(int id);
    Task<Scaler> FindByIdEmailAndPasswordAsync(string email, string password);
    void Update(Scaler category);
    void Delete(Scaler category);
}