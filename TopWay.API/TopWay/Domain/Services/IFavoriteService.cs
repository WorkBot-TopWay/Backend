using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface IFavoriteService
{
    Task<IEnumerable<Favorite>> ListAsync();
    Task<IEnumerable<Favorite>> FindByScalerIdAsync(int scalerId);
    Task<IEnumerable<ClimbingGyms>> FindClimbingGymByScalerIdAsync(int scalerId);
    Task<Favorite> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
    Task<Favorite> FindByIdAsync(int id);
    Task<FavoriteResponse> AddAsync(Favorite favorite, int climbingGymId, int scalerId);
    Task<FavoriteResponse> UpdateAsync(Favorite favorite, int climbingGymId, int scalerId);
    Task<FavoriteResponse> Delete(int climbingGymId, int scalerId);
}