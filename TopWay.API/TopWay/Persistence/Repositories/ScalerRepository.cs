using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class ScalerRepository : BaseRepository, IScalerRepository
{
    public ScalerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Scaler>> ListAsync()
    {
        return await _context.Scalers.ToListAsync();
    }

    public async Task AddAsync(Scaler scaler)
    {
        await _context.Scalers.AddAsync(scaler);
    }

    public async Task<Scaler> FindByIdAsync(int id)
    {
        return (await _context.Scalers.FindAsync(id))!;
    }
    

    public void Update(Scaler category)
    {
       _context.Scalers.Update(category);
    }

    public void Delete(Scaler category)
    {
        _context.Scalers.Remove(category);
    }
}