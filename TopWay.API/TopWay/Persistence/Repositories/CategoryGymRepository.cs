using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CategoryGymRepository: BaseRepository,ICategoryGymRepository
{
    public CategoryGymRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CategoryGym>> GetAll()
    {
        return await _context.CategoriesGyms.ToListAsync();
    }

    public async Task<IEnumerable<ClimbingGym>> FindClimbingGymsByCategoryIdAsync(int categoryId)
    {
        return await _context.ClimbingGyms
            .Include(c => c.CategoryGyms)
            .Where(c => c.CategoryGyms.Any(cg => cg.CategoryId == categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> FindCategoriesByGymIdAsync(int gymId)
    {
        return await _context.Categories
            .Include(c=>c.CategoryGym)
            .Where(c => c.CategoryGym.Any(cg => cg.ClimbingGymId == gymId))
            .ToListAsync();
    }

    public async Task<CategoryGym> FindByCategoryIdAndClimbingGymIdAsync(int categoryId, int climbingGymId)
    {
        return (await _context.CategoriesGyms
            .Where(cg => cg.CategoryId == categoryId && cg.ClimbingGymId == climbingGymId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CategoryGym> FindByIdAsync(int id)
    {
       return (await  _context.CategoriesGyms.FindAsync(id))!;
    }

    public async Task AddAsync(CategoryGym categoryGym)
    {
        await _context.CategoriesGyms.AddAsync(categoryGym);
    }

    public void Delete(CategoryGym categoryGym)
    {
        _context.CategoriesGyms.Remove(categoryGym);
    }
}