using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IClimbingGymService
{
    Task<IEnumerable<ClimbingGym>> ListAsync();
    Task<ClimbingGym> FindByIdAsync(int id);
    Task<ClimbingGymResponse> SaveAsync(ClimbingGym climbingGym);
    Task<ClimbingGymResponse> UpdateAsync(int id, ClimbingGym climbingGym);
    Task<ClimbingGymResponse> DeleteAsync(int id);
}