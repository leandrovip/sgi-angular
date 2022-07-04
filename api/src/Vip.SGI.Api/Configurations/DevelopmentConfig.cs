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
        }
    }

    #endregion
}