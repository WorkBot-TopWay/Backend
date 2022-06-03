using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class ClimbingGymResponse : BaseResponse<ClimbingGym>
{
    public ClimbingGymResponse(ClimbingGym resource) : base(resource)
    {
    }

    public ClimbingGymResponse(string message) : base(message)
    {
    }
}