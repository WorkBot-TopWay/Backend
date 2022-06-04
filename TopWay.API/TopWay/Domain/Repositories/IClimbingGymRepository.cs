using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IClimbingGymRepository
{
    Task<IEnumerable<ClimbingGym>> ListAsync();
    Task AddAsync(ClimbingGym climbingGym);
    Task<ClimbingGym> FindByIdAsync(int id);
    Task<ClimbingGym> FindByNameAsync(string name);
    void Update(ClimbingGym climbingGym);
    void Delete(ClimbingGym climbingGym);
}