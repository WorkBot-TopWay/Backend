using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface IFavoriteRepository
{
   Task<IEnumerable<Favorite>> ListAsync();
   Task<IEnumerable<Favorite>> FindByScalerIdAsync(int scalerId);
   Task<IEnumerable<ClimbingGyms>> FindClimbingGymByScalerIdAsync(int scalerId);
   Task<Favorite> FindByClimbingGymIdAndScalerIdAsync(int climbingGymId, int scalerId);
   Task<Favorite> FindByIdAsync(int id);
   Task AddAsync(Favorite favorite);
   void Update(Favorite favorite);
   void Delete(Favorite favorite);
}