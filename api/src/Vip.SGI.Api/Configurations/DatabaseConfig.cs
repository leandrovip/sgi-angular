using Microsoft.EntityFrameworkCore;
using Vip.SGI.Infra.Context;

namespace Vip.SGI.Api.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabase(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<SgiContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("sgicontext"));
        });
    }
}