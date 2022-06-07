using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class ClimbersLeagueResponse: BaseResponse<ClimberLeagues>
{
    public ClimbersLeagueResponse(ClimberLeagues resource) : base(resource)
    {
    }

    public ClimbersLeagueResponse(string message) : base(message)
    {
    }
}