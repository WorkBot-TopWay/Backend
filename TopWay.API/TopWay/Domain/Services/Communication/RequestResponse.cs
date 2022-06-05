using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class RequestResponse:BaseResponse<Request>
{
    public RequestResponse(Request resource) : base(resource)
    {
    }

    public RequestResponse(string message) : base(message)
    {
    }
}