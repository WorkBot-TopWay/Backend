using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> ListAsync();
    Task<Category> FindByIdAsync(int id);
    Task<Category> FindByNameAsync(string name);
    Task<CategoryResponse> SaveAsync(Category climbingGym);
    Task<CategoryResponse> UpdateAsync(int id, Category climbingGym);
    Task<CategoryResponse> DeleteAsync(int id);
}