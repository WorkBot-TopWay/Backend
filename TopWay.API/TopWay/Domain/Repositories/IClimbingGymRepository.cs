using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IClimbingGymRepository
{
    Task<IEnumerable<ClimbingGyms>> ListAsync();
    Task AddAsync(ClimbingGyms climbingGyms);
    Task<ClimbingGyms> FindByIdAsync(int id);
    Task<ClimbingGyms> LogIn(string email, string password);
    Task<ClimbingGyms> FindByNameAsync(string name);
    void Update(ClimbingGyms climbingGyms);
    void Delete(ClimbingGyms climbingGyms);
}