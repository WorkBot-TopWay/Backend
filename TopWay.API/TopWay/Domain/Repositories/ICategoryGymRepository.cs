using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICategoryGymRepository
{
    Task<IEnumerable<CategoryGym>> GetAll();
    Task<IEnumerable<ClimbingGym>> FindClimbingGymsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Category>> FindCategoriesByGymIdAsync(int gymId);
    
    Task<CategoryGym> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId);
    Task<CategoryGym> FindByIdAsync(int id);
    Task AddAsync(CategoryGym categoryGym);
    void Delete(CategoryGym categoryGym);
}