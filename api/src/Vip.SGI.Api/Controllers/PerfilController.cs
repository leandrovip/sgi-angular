using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vip.SGI.Api.Controllers.Base;
using Vip.SGI.Api.Security;
using Vip.SGI.Application.Adapters;
using Vip.SGI.Application.Dto;
using Vip.SGI.Application.Services;
using Vip.SGI.Application.Wrapper;

namespace Vip.SGI.Api.Controllers;

[Authorize]
public class PerfilController : BaseController
{
    #region Propriedades

    private readonly UsuarioService _service;
    private readonly ICurrentUser _currentUser;

    #endregion

    #region Construtores

    public PerfilController(UsuarioService service, ICurrentUser currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    #endregion

    #region Métodos Públicos

    [HttpGet]
    public async Task<IActionResult> Obter()
    {
        if (_currentUser.IsNotAutenticated()) return BadResult("Usuário não autenticado");
        var usuario = await _service.Obter(_currentUser.GetUserId());
        return usuario.IsNull() ? BadResult("Usuário não encontrado") : ActionResult(_service, usuario.ToDto());
    }

    [HttpPut]
    public async Task<IActionResult> Editar(UsuarioDto dto)
    {
        if (dto.IsNotNull() && dto.Senha.IsNullOrEmpty()) dto.Senha = "hack@Vip";
        var usuario = dto.ToEntity();
        if (usuario.Invalid) return BadRequest(ResultFactory.InvalidModel(usuario));
        await _service.Salvar(usuario, false);
        return ActionResult(_service, usuario.ToDto());
    }

    [HttpPost]
    [Route("autenticar")]
    [AllowAnonymous]
    public async Task<IActionResult> Autenticar(UsuarioLoginDto tokenRequest)
    {
        if (tokenRequest.IsNull()) return BadResult("E-mail ou senha inválidos");
        var usuario = await _service.Obter(tokenRequest.Email, tokenRequest.Senha);
        return usuario.IsNull() ? BadResult("E-mail ou senha inválidos") : ActionResult(_service, usuario.ToAccessToken());
    }

    #endregion
}