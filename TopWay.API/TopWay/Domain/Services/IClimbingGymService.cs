using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IClimbingGymService
{
    Task<IEnumerable<ClimbingGyms>> ListAsync();
    Task<ClimbingGyms> FindByIdAsync(int id);
    Task<ClimbingGyms> LogIn(string email, string password);
    Task<ClimbingGyms> FindByNameAsync(string name);
    Task<ClimbingGymResponse> SaveAsync(ClimbingGyms climbingGyms);
    Task<ClimbingGymResponse> UpdateAsync(int id, ClimbingGyms climbingGyms);
    Task<ClimbingGymResponse> DeleteAsync(int id);
}