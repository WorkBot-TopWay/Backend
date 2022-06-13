using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CategoryGymRepository: BaseRepository,ICategoryGymRepository
{
    public CategoryGymRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CategoryGyms>> GetAll()
    {
        return await _context.CategoriesGyms.ToListAsync();
    }

    public async Task<IEnumerable<ClimbingGyms>> FindClimbingGymsByCategoryIdAsync(int categoryId)
    {
        return await _context.ClimbingGyms
            .Include(c => c.CategoryGyms)
            .Where(c => c.CategoryGyms.Any(cg => cg.CategoryId == categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Categories>> FindCategoriesByGymIdAsync(int gymId)
    {
        return await _context.Categories
            .Include(c=>c.CategoryGym)
            .Where(c => c.CategoryGym.Any(cg => cg.ClimbingGymId == gymId))
            .ToListAsync();
    }

    public async Task<CategoryGyms> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId)
    {
        return (await _context.CategoriesGyms
            .Where(cg => cg.CategoryId == categoryId && cg.ClimbingGymId == climbingGymId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CategoryGyms> FindByIdAsync(int id)
    {
       return (await  _context.CategoriesGyms.FindAsync(id))!;
    }

    public async Task AddAsync(CategoryGyms categoryGyms)
    {
        await _context.CategoriesGyms.AddAsync(categoryGyms);
    }

    public void Delete(CategoryGyms categoryGyms)
    {
        _context.CategoriesGyms.Remove(categoryGyms);
    }
}