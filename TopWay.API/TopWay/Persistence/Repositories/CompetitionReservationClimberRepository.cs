using Microsoft.EntityFrameworkCore;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Persistence.Contexts;

namespace TopWay.API.TopWay.Persistence.Repositories;

public class CompetitionReservationClimberRepository : BaseRepository, ICompetitionReservationClimberRepository
{
    public CompetitionReservationClimberRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CompetitionReservationClimber>> ListAsync()
    {
        return await _context.CompetitionReservationClimbers.ToListAsync();
    }

    public async Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId)
    {
        return await _context.Scalers
            .Include(s => s.CompetitionReservationClimbers)
            .ThenInclude(c => c.CompetitionGyms)
            .Where(s => s.CompetitionReservationClimbers.Any(c => c.CompetitionGymId == competitionId))
            .ToListAsync();
    }

    public async Task<IEnumerable<CompetitionReservationClimber>> FindByCompetitionIdAsync(int competitionId)
    {
        return await _context.CompetitionReservationClimbers
            .Include(c => c.CompetitionGyms)
            .Where(c => c.CompetitionGymId == competitionId)
            .ToListAsync();
    }


    public async Task<CompetitionReservationClimber> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId)
    {
        return (await _context.CompetitionReservationClimbers
            .Where(c => c.CompetitionGymId == competitionId && c.ScalerId == scalerId)
            .FirstOrDefaultAsync())!;
    }

    public async Task<CompetitionReservationClimber> FindByIdAsync(int id)
    {
        return (await _context.CompetitionReservationClimbers.FindAsync(id))!;
    }

    public async Task AddAsync(CompetitionReservationClimber competitionReservationClimber)
    {
        await _context.CompetitionReservationClimbers.AddAsync(competitionReservationClimber);
    }

    public void Delete(CompetitionReservationClimber competitionReservationClimber)
    {
        _context.CompetitionReservationClimbers.Remove(competitionReservationClimber);
    }
}