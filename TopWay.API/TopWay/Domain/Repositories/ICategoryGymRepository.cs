using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICategoryGymRepository
{
    Task<IEnumerable<CategoryGyms>> GetAll();
    Task<IEnumerable<ClimbingGyms>> FindClimbingGymsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Categories>> FindCategoriesByGymIdAsync(int gymId);
    
    Task<CategoryGyms> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId);
    Task<CategoryGyms> FindByIdAsync(int id);
    Task AddAsync(CategoryGyms categoryGyms);
    void Delete(CategoryGyms categoryGyms);
}