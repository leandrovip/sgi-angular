﻿using Microsoft.EntityFrameworkCore;
using Vip.SGI.Application.Services.Base;
using Vip.SGI.Domain.Models;
using Vip.SGI.Infra.Context;
using Vip.SGI.Shared.Extensions;

namespace Vip.SGI.Application.Services;

public class UsuarioService : BaseService
{
    #region Propriedades

    #endregion

    #region Construtores

    public UsuarioService(SgiContext context) : base(context) { }

    #endregion

    #region Métodos Públicos

    public async Task<bool> Salvar(Usuario usuario, bool novoCadastro)
    {
        if (InvalidModel(usuario)) return false;
        if (await SeCadastrado(usuario)) return false;
        if (!novoCadastro) await CarregarUsuario(usuario);

        usuario.Criptografar();
        return await Salvar<Usuario>(usuario, novoCadastro);
    }

    public async Task Excluir(Guid usuarioId)
    {
        await Excluir<Usuario>(usuarioId, "Usuário não encontrado");
    }

    public async Task<Usuario> Obter(Guid usuarioId)
    {
        if (usuarioId.IsEmpty())
        {
            AddNotification("UsuarioService.Obter", "Usuário não encontrado");
            return null;
        }

        try
        {
            return await _db.Usuario.FindAsync(usuarioId);
        }
        catch (Exception ex)
        {
            AddNotification("UsuarioService.Obter", "Não foi possível obter o usuário desejado");
            AddNotification("Erro", $"Mensagem: {ex.Message}");
            return null;
        }
    }

    public async Task<Usuario> Obter(string email, string senha)
    {
        if (email.IsNullOrEmpty() || senha.IsNullOrEmpty())
        {
            AddNotification("UsuarioService.Obter", "E-mail ou senha inválidos");
            return null;
        }

        try
        {
            email = email.ToLower().TrimVip();
            senha = senha.Encrypt();
            return await _db.Usuario.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Endereco == email && x.Senha == senha);
        }
        catch (Exception ex)
        {
            AddNotification("UsuarioService.Obter", "Não foi possível obter o usuário desejado");
            AddNotification("Erro", $"Mensagem: {ex.Message}");
            return null;
        }
    }

    public async Task<IEnumerable<Usuario>> Obter()
    {
        return await _db.Usuario.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
    }

    #endregion

    #region Métodos Privados

    private async Task CarregarUsuario(Usuario usuario)
    {
        var cadastro = await _db.Usuario.AsNoTracking().FirstOrDefaultAsync(x => x.UsuarioId.Equals(usuario.UsuarioId));
        usuario.PrepararEdicao(cadastro!);
    }

    private async Task<bool> SeCadastrado(Usuario usuario)
    {
        try
        {
            var retorno = await _db.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Endereco.Equals(usuario.Email.Endereco));

            if (retorno.IsNull()) return false;

            if (usuario.UsuarioId != retorno.UsuarioId)
                AddNotification("UsuarioService.SeCadastrado", "Usuário já cadastrado");

            return usuario.UsuarioId != retorno.UsuarioId;
        }
        catch (Exception ex)
        {
            AddNotification("UsuarioService.SeCadastrado", "Não foi possível validar o usuário");
            AddNotification("Erro", $"Mensagem: {ex.Message}");
            return false;
        }
    }

    #endregion
}