using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class ImagesResponse: BaseResponse<Images>
{
    public ImagesResponse(Images resource) : base(resource)
    {
    }

    public ImagesResponse(string message) : base(message)
    {
    }
}