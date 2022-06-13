using Microsoft.EntityFrameworkCore;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CategoryRepository: BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categories>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Categories climbingGym)
    {
        await _context.Categories.AddAsync(climbingGym);
    }

    public async Task<Categories> FindByIdAsync(int id)
    {
        return (await _context.Categories.FindAsync(id))!;
    }

    public async Task<Categories> FindByNameAsync(string name)
    {
        return (await _context.Categories
            .Where(p=>p.Name == name)
            .FirstOrDefaultAsync())!;
    }

    public void Update(Categories climbingGym)
    {
        _context.Categories.Update(climbingGym);
    }

    public void Delete(Categories climbingGym)
    {
        _context.Categories.Remove(climbingGym);
    }
}