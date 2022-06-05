using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class RequestRepository: BaseRepository, IRequestRepository
{
    public RequestRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Request>> GetAll()
    {
        return await _context.Requests.ToListAsync();
    }

    public async Task<Request> FindLeagueIdAndScapeId(int leagueId, int scapeId)
    {
        return (await _context.Requests.FirstOrDefaultAsync(x => x.LeagueId == leagueId && x.ScalerId == scapeId))!;
    }

    public async Task<IEnumerable<Scaler>> FindRequestScalerByLeagueId(int leagueId)
    {
       return await _context.Scalers
           .Include(s => s.Requests)
           .Where(s => s.Requests.Any(r => r.LeagueId == leagueId))
           .ToListAsync();
           
    }

    public async Task<Request> FindByIdAsync(int id)
    {
        return (await _context.Requests.FindAsync(id))!;
    }

    public async Task AddAsync(Request request)
    {
        await _context.Requests.AddAsync(request);
    }

    public void Delete(Request request)
    {
        _context.Requests.Remove(request);
    }
}