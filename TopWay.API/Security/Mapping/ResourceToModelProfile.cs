using AutoMapper;
using Org.BouncyCastle.Asn1.X509;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Services.Communication;

namespace TopWay.API.Security.Mapping;

public class ResourceToModelProfile :Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, Scaler>();
        CreateMap<UpdateRequest, Scaler>().ForAllMembers(options => 
            options.Condition((source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
                
            ));
    }
}