using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Vip.SGI.Api.Security;
using Vip.SGI.Application.Wrapper;
using Vip.SGI.Shared.Constants;

namespace Vip.SGI.Api.Configurations;

public static class SecurityConfig
{
    #region Services

    public static void AddJwtAuth(this IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes(Settings.JwtSecret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
                bearer.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Garante sempre uma mensagem de erro.
                        if (context.Error.IsNullOrEmpty()) context.Error = "invalid_token";
                        if (context.ErrorDescription.IsNullOrEmpty()) context.ErrorDescription = "Esta solicitação requer um token válido";

                        // Verifica se é um token expirado
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authenticationException?.Expires.ToString("o"));
                            context.ErrorDescription = $"Token inválido! Expirado em {authenticationException?.Expires:o}";
                        }

                        var response = ResultFactory.Unauthorized(context.ErrorDescription ?? "Não autorizado");
                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },
                    OnForbidden = context =>
                    {
                        var response = ResultFactory.Unauthorized("Acesso não autorizado");
                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                };
            });
    }

    public static void AddCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    #endregion
}