using AutoMapper;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveScalerResource, Scaler>();
    }
}