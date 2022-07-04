using Microsoft.AspNetCore.Mvc;
using Vip.SGI.Api.Controllers.Base;

namespace Vip.SGI.Api.Controllers;

public class HealthCheckController : BaseController
{
    [HttpGet]
    public string Get() => DateTime.Now.ToString("O");
}