using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class NewsResponse : BaseResponse<News>
{
    public NewsResponse(News resource) : base(resource)
    {
    }

    public NewsResponse(string message) : base(message)
    {
    }
}