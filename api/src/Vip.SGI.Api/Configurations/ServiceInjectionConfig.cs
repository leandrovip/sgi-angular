using Vip.SGI.Api.Middlewares;
using Vip.SGI.Api.Security;
using Vip.SGI.Application.Services;
using Vip.SGI.Infra.Context;

namespace Vip.SGI.Api.Configurations
{
    public static class ServiceInjectionConfig
    {
        public static void AddServiceInjection(this IServiceCollection services)
        {
            #region Middleware

            services.AddTransient<ErrorHandlerMiddleware>();

            #endregion

            #region Services

            services.AddTransient<UsuarioService>();

            #endregion

            #region Infra

            services.AddScoped<SgiContext>();

            #endregion
        }
    }
}