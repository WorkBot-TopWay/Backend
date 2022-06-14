using System.Dynamic;
using Microsoft.Extensions.Options;
using TopWay.API.Security.Authorization.Handlers.Interfaces;
using TopWay.API.Security.Authorization.Settings;
using TopWay.API.Security.Domain.Services;

namespace TopWay.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(IOptions<AppSettings> appSettings, RequestDelegate next)
    {
        _appSettings = appSettings.Value;
        _next = next;
    }

    public async Task Invoke(HttpContext context, IScalerService scalerService, IJwtHandler jwtHandler)
    {
        var token = context.Request.Headers["Authorization"];
        var userId = jwtHandler.ValidateToken(token);
        if (userId != null)
        {
            //Attach user to context on successful jwt validation
            context.Items["Scaler"] = scalerService.FindByIdAsync(userId.Value);
        }

        await _next(context);
    }
}