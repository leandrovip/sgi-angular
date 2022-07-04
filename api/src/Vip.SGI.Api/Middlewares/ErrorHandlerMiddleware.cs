using System.Net;
using Vip.SGI.Application.Wrapper;
using Vip.SGI.Shared.Exceptions;
using Vip.SGI.Shared.Extensions;

namespace Vip.SGI.Api.Middlewares;

public class  ErrorHandlerMiddleware : IMiddleware
{
    #region Métodos

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    SgiException => (int) HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int) HttpStatusCode.NotFound,
                    _ => (int) HttpStatusCode.InternalServerError
                };

                var responseModel = ResultFactory.BadRequest(error);
                await response.WriteAsync(responseModel.ToJson());
            }
            else
            {
                await response.WriteAsync(string.Empty);
            }
        }
    }

    #endregion
}