using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CompetitionLeagueRankingResponse: BaseResponse<CompetitionLeagueRanking>
{
    public CompetitionLeagueRankingResponse(CompetitionLeagueRanking resource) : base(resource)
    {
    }

    public CompetitionLeagueRankingResponse(string message) : base(message)
    {
    }
}