using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class LeagueResponse:BaseResponse<League>
{
    public LeagueResponse(League resource) : base(resource)
    {
    }

    public LeagueResponse(string message) : base(message)
    {
    }
}