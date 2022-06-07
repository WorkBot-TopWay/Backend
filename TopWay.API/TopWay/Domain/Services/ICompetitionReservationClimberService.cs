using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Domain.Services.Communication;

namespace TopWay.API.TopWay.Domain.Services;

public interface ICompetitionReservationClimberService
{
    Task<IEnumerable<CompetitionReservationClimber>> ListAsync();
    Task<IEnumerable<Scaler>> FindScalerByCompetitionIdAsync(int competitionId);
    Task<IEnumerable<CompetitionReservationClimber>> FindByCompetitionIdAsync(int competitionId);
    Task<CompetitionReservationClimber> FindByCompetitionIdAndScalerIdAsync(int competitionId, int scalerId);
    Task<CompetitionReservationClimber> FindByIdAsync(int id);
    Task<CompetitionReservationClimberResponse> AddAsync(CompetitionReservationClimber competitionReservationClimber,int competitionId, int scalerId);

    Task<CompetitionReservationClimberResponse> Delete(int competitionId, int scalerId);
}