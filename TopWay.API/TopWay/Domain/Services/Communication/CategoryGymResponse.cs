using TopWay.API.Shared.Domain.Services.Communication;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Domain.Services.Communication;

public class CategoryGymResponse: BaseResponse<CategoryGym>
{
    public CategoryGymResponse(CategoryGym resource) : base(resource)
    {
    }

    public CategoryGymResponse(string message) : base(message)
    {
    }
}