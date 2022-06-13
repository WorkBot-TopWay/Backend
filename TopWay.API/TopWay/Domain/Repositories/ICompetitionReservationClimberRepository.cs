using TopWay.API.Security.Domain.Models;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Repositories;

public interface ICompetitionReservationClimberRepository
{
    Task<IEnumerable<CompetitionReservationClimber>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionReservationClimber>> FindByCompetitionIdAsync(int competitionId);
    Task<CompetitionReservationClimber> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionReservationClimber> FindByIdAsync(int id);
    Task AddAsync(CompetitionReservationClimber competitionReservationClimber);
    void Delete(CompetitionReservationClimber competitionReservationClimber);
}