using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CompetitionLeagueResponse: BaseResponse<CompetitionLeague>
{
    public CompetitionLeagueResponse(CompetitionLeague resource) : base(resource)
    {
    }

    public CompetitionLeagueResponse(string message) : base(message)
    {
    }
}