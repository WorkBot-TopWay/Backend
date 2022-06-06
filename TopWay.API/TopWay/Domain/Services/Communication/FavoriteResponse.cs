using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class FavoriteResponse: BaseResponse<Favorite>
{
    public FavoriteResponse(Favorite resource) : base(resource)
    {
    }

    public FavoriteResponse(string message) : base(message)
    {
    }
}