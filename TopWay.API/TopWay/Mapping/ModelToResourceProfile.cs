using AutoMapper;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Scaler, ScalerResource>();
    }
}