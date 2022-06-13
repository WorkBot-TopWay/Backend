using TopWay.API.Security.Domain.Models;
using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.Security.Domain.Services.Communication;

public class ScalerResponse : BaseResponse<Scaler>
{
    public ScalerResponse(Scaler resource) : base(resource)
    {
    }

    public ScalerResponse(string message) : base(message)
    {
    }
}