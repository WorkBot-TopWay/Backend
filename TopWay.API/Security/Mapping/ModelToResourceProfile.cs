using AutoMapper;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Services.Communication;
using TopWay.API.Security.Resources;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Scaler, ScalerResource>();
        CreateMap<Scaler, AuthenticateResponse>();
    }
}