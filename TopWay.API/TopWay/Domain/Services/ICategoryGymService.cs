using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICategoryGymService
{
    Task<IEnumerable<CategoryGym>> GetAll();
    Task<IEnumerable<ClimbingGym>> FindClimbingGymsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Category>> FindCategoriesByGymIdAsync(int gymId);
    Task<CategoryGym> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId);
    Task<CategoryGym> FindByIdAsync(int id);
    Task<CategoryGymResponse> SaveAsync(CategoryGym categoryGym, int climbingGymId, int categoryId);
    Task<CategoryGymResponse> DeleteAsync(int climbingGymId, int categoryId);
}