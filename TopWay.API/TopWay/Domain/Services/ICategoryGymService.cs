using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICategoryGymService
{
    Task<IEnumerable<CategoryGyms>> GetAll();
    Task<IEnumerable<ClimbingGyms>> FindClimbingGymsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Categories>> FindCategoriesByGymIdAsync(int gymId);
    Task<CategoryGyms> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId);
    Task<CategoryGyms> FindByIdAsync(int id);
    Task<CategoryGymResponse> SaveAsync(CategoryGyms categoryGyms, int climbingGymId, int categoryId);
    Task<CategoryGymResponse> DeleteAsync(int climbingGymId, int categoryId);
}