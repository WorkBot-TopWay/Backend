using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> ListAsync();
    Task AddAsync(Category climbingGym);
    Task<Category> FindByIdAsync(int id);
    Task<Category> FindByNameAsync(string name);
    void Update(Category climbingGym);
    void Delete(Category climbingGym);
}