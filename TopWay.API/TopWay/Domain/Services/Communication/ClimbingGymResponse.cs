using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class ClimbingGymResponse : BaseResponse<ClimbingGyms>
{
    public ClimbingGymResponse(ClimbingGyms resource) : base(resource)
    {
    }

    public ClimbingGymResponse(string message) : base(message)
    {
    }
}