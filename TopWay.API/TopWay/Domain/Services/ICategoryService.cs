using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICategoryService
{
    Task<IEnumerable<Categories>> ListAsync();
    Task<Categories> FindByIdAsync(int id);
    Task<Categories> FindByNameAsync(string name);
    Task<CategoryResponse> SaveAsync(Categories climbingGym);
    Task<CategoryResponse> UpdateAsync(int id, Categories climbingGym);
    Task<CategoryResponse> DeleteAsync(int id);
}