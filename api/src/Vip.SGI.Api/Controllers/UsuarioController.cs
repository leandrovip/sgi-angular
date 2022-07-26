﻿using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<IActionResult> Obter()
    {
        var usuarios = await _service.Obter();
        return ActionResult(_service, usuarios.ToDto());
    }

    [HttpGet("{usuarioId:guid}")]
    public async Task<ActionResult> ObterPorId(Guid usuarioId)
    {
        var usuario = await _service.Obter(usuarioId);
        return ActionResult(_service, usuario.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Incluir(UsuarioDto dto)
    {
        var usuario = dto.ToEntity();
        if (usuario.Invalid) return BadRequest(ResultFactory.InvalidModel(usuario));
        await _service.Salvar(usuario, true);
        return ActionResult(_service, usuario.ToDto(), "Usuário salvo com sucesso");
    }

    [HttpPut]
    public async Task<IActionResult> Editar(UsuarioDto dto)
    {
        if (dto.IsNotNull() && dto.Senha.IsNullOrEmpty()) dto.Senha = "hack@Vip";
        var usuario = dto.ToEntity();
        if (usuario.Invalid) return BadRequest(ResultFactory.InvalidModel(usuario));
        await _service.Salvar(usuario, false);
        return ActionResult(_service, usuario.ToDto(), "Usuário salvo com sucesso");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        await _service.Excluir(id);
        return ActionResult(_service, message: "Usuário excluído com sucesso");
    }

    #endregion
}