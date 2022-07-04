using Vip.SGI.Api.Configurations;
using Vip.SGI.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

#region Services

// TODO: Adicionar SignalR posteriormente para comunicação
// TODO: Adicionar classe shared de email e datetime service
// TODO: Adicionar LazyCache

builder.Services.AddCors();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServiceInjection();
builder.Services.AddJwtAuth();
builder.Services.AddCurrentUser();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#endregion

#region ApplicationBuilder

var app = builder.Build();
app.UseCors();
app.UseDevelopment(app.Environment);
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRoutes();

#endregion

app.Run();