using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CompetitionGymResponse : BaseResponse<CompetitionGym>
{
    public CompetitionGymResponse(CompetitionGym resource) : base(resource)
    {
    }

    public CompetitionGymResponse(string message) : base(message)
    {
    }
}