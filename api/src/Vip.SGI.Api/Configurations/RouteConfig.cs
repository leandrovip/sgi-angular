namespace Vip.SGI.Api.Configurations;

public static class RouteConfig
{
    public static void UseRoutes(this IApplicationBuilder app)
    {
        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}