using Vip.SGI.Application.Dto;
using Vip.SGI.Application.Security;
using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Shared.Helpers;

namespace Vip.SGI.Application.Adapters;

public static class UsuarioAdapter
{
    public static Usuario ToEntity(this UsuarioDto dto)
    {
        var funcao = EnumHelper.GetEnum<UsuarioFuncao>(dto.UsuarioFuncao);
        return new Usuario(dto.UsuarioId, dto.Nome, dto.Email, dto.Senha, funcao);
    }

    public static UsuarioDto ToDto(this Usuario usuario)
    {
        return new UsuarioDto(usuario.UsuarioId, usuario.Nome, usuario.Email.Endereco, "", usuario.UsuarioFuncao.GetDescription());
    }

    public static IEnumerable<UsuarioDto> ToDto(this IEnumerable<Usuario> usuarios)
    {
        return usuarios.Select(x => x.ToDto()).ToList();
    }

    public static UsuarioTokenDto ToToken(this Usuario usuario)
    {
        var token = JwtBuilder.Create(usuario);
        return new UsuarioTokenDto(usuario.UsuarioId, usuario.Nome, usuario.Email.Endereco, token);
    }
}