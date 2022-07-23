using Vip.SGI.Infra.Context;

namespace Vip.SGI.Api.Configurations;

public static class DevelopmentConfig
{
    #region ApplicationBuilder

    public static void UseDevelopment(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(o => { o.DocumentTitle = "Swagger VIP - Sistema de Gerenciamento Interno"; });

            #region Seed Migrations

            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<SgiContext>();
            context?.Seed();

            #endregion
        }
    }

    #endregion
}