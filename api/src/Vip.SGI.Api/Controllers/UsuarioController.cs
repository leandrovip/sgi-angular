using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vip.SGI.Api.Controllers.Base;
using Vip.SGI.Application.Adapters;
using Vip.SGI.Application.Dto;
using Vip.SGI.Application.Services;
using Vip.SGI.Application.Wrapper;
using Vip.SGI.Shared.Constants;

namespace Vip.SGI.Api.Controllers;

[Authorize(Roles = PermissionRole.Administrador)]
public class UsuarioController : BaseController
{
    #region Propriedades

    private readonly UsuarioService _service;

    #endregion

    #region Construtores

    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    #endregion

    #region Métodos Públicos

    [HttpPost]
    public async Task<IActionResult> Incluir(UsuarioDto dto)
    {
        var usuario = dto.ToEntity();
        if (usuario.Invalid) return BadRequest(ResultFactory.InvalidModel(usuario));
        await _service.Salvar(usuario, true);
        return ActionResult(_service, usuario.ToDto());
    }

    [HttpPut]
    public async Task<IActionResult> Editar(UsuarioDto dto)
    {
        var usuario = dto.ToEntity();
        if (usuario.Invalid) return BadRequest(ResultFactory.InvalidModel(usuario));
        await _service.Salvar(usuario, false);
        return ActionResult(_service, usuario.ToDto());
    }

    [HttpDelete]
    public async Task<IActionResult> Excluir(Guid id)
    {
        await _service.Excluir(id);
        return ActionResult(_service);
    }

    [HttpPost]
    [Route("~/token")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterToken(UsuarioLoginDto tokenRequest)
    {
        if (tokenRequest.IsNull()) return BadResult("E-mail ou senha inválidos");
        var usuario = await _service.Obter(tokenRequest.Email, tokenRequest.Senha);
        return usuario.IsNull() ? BadResult("E-mail ou senha inválidos") : ActionResult(_service, usuario.ToToken());
    }

    #endregion
}