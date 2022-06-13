using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class FeatureRepository: BaseRepository,IFeatureRepository
{
    public FeatureRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Features> FindByIdAsync(int id)
    {
        return (await _context.Features.FindAsync(id))!;
    }

    public async Task AddAsync(Features features)
    {
        await _context.Features.AddAsync(features);
    }

    public void Update(Features features)
    {
        _context.Features.Update(features);
    }

    public void Delete(Features features)
    {
        _context.Features.Remove(features);
    }
}