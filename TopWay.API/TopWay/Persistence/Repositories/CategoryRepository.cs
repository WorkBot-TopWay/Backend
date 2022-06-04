using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CategoryRepository: BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Category climbingGym)
    {
        await _context.Categories.AddAsync(climbingGym);
    }

    public async Task<Category> FindByIdAsync(int id)
    {
        return (await _context.Categories.FindAsync(id))!;
    }

    public async Task<Category> FindByNameAsync(string name)
    {
        return (await _context.Categories
            .Where(p=>p.Name == name)
            .FirstOrDefaultAsync())!;
    }

    public void Update(Category climbingGym)
    {
        _context.Categories.Update(climbingGym);
    }

    public void Delete(Category climbingGym)
    {
        _context.Categories.Remove(climbingGym);
    }
}