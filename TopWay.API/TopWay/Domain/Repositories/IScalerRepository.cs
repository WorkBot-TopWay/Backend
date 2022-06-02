using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IScalerRepository
{
    Task<IEnumerable<Scaler>> ListAsync();
    Task AddAsync(Scaler category);
    Task<Scaler> FindByIdAsync(int id);
    void Update(Scaler category);
    void Delete(Scaler category);
}