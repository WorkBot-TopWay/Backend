using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CommentResponse: BaseResponse<Comments>
{
    public CommentResponse(Comments resource) : base(resource)
    {
    }

    public CommentResponse(string message) : base(message)
    {
    }
}