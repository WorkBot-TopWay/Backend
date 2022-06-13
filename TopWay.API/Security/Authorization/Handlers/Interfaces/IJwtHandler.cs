using TopWay.API.Security.Domain.Models;

namespace TopWay.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(Scaler scaler);
    int? ValidateToken(string token);
}