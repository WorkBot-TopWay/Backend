using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CompetitionReservationClimberResponse:BaseResponse<CompetitionReservationClimber>
{
    public CompetitionReservationClimberResponse(CompetitionReservationClimber resource) : base(resource)
    {
    }

    public CompetitionReservationClimberResponse(string message) : base(message)
    {
    }
}