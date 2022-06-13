using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.Security.Domain.Repositories;

public interface IScalerRepository
{
    Task<IEnumerable<Scaler>> ListAsync();
    Task AddAsync(Scaler category);
    Task<Scaler> FindByIdAsync(int id);
    Task<Scaler> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);
    Task<Scaler> FindByIdEmailAndPasswordAsync(string email, string password);
    void Update(Scaler category);
    void Delete(Scaler category);
}