using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class FeatureResponse: BaseResponse<Features>
{
    public FeatureResponse(Features resource) : base(resource)
    {
    }

    public FeatureResponse(string message) : base(message)
    {
    }
}