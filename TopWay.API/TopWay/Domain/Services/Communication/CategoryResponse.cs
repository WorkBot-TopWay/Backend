using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CategoryResponse: BaseResponse<Categories>
{
    public CategoryResponse(Categories resource) : base(resource)
    {
    }

    public CategoryResponse(string message) : base(message)
    {
    }
}