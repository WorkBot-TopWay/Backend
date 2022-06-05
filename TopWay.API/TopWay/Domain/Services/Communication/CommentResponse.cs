using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CommentResponse: BaseResponse<Comment>
{
    public CommentResponse(Comment resource) : base(resource)
    {
    }

    public CommentResponse(string message) : base(message)
    {
    }
}