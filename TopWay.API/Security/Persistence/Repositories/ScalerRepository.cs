using Microsoft.EntityFrameworkCore;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Repositories;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.Security.Persistence.Repositories;

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

    public async Task<Scaler> FindByEmailAsync(string email)
    {
        return (await _context.Scalers.FirstOrDefaultAsync(x => x.Email == email))!;
    }

    public bool ExistsByEmail(string email)
    {
        return _context.Scalers.Any(x => x.Email == email);
    }


    public async Task<Scaler> FindByIdEmailAndPasswordAsync(string email, string password)
    {
        return (await _context.Scalers.FirstOrDefaultAsync(s => s.Email == email && s.PasswordHash == password))!;
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