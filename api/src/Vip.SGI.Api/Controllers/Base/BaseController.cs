using Microsoft.AspNetCore.Mvc;
using Vip.SGI.Application.Wrapper;
using Vip.Validator.Notifications;

namespace Vip.SGI.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    #region Métodos de Retorno

    protected ObjectResult ApplicationError(string message)
    {
        return StatusCode(500, ResultFactory.ApplicationError(message));
    }

    protected ObjectResult ActionResult(Notifiable model, object data = null, string message = "")
    {
        return model.Valid ? Ok(ResultFactory.Success(message, data)) : BadRequest(ResultFactory.BadRequest(model.Notifications));
    }

    protected ObjectResult ActionResultPaged(Notifiable model, object data)
    {
        return model.Valid ? Ok(data) : BadRequest(data);
    }

    protected ObjectResult BadResult(string message, object data = null)
    {
        return BadRequest(ResultFactory.BadRequest(message, data));
    }

    #endregion
}