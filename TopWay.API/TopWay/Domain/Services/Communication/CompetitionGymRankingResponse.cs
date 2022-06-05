using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CompetitionGymRankingResponse: BaseResponse<CompetitionGymRanking>
{
    public CompetitionGymRankingResponse(CompetitionGymRanking resource) : base(resource)
    {
    }

    public CompetitionGymRankingResponse(string message) : base(message)
    {
    }
}