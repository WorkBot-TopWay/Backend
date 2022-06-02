using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}