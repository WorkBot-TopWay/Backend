using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Categories>> ListAsync();
    Task AddAsync(Categories climbingGym);
    Task<Categories> FindByIdAsync(int id);
    Task<Categories> FindByNameAsync(string name);
    void Update(Categories climbingGym);
    void Delete(Categories climbingGym);
}