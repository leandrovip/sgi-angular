using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vip.SGI.Domain.Models;
using Vip.SGI.Shared.Constants;

namespace Vip.SGI.Application.Security;

internal static class JwtBuilder
{
    public static string Create(Usuario usuario)
    {
        return usuario.IsNull() ? string.Empty : GenerateEncryptedToken(usuario);
    }

    private static string GenerateEncryptedToken(Usuario usuario)
    {
        #region Generate SigningCredentials

        var secret = Encoding.UTF8.GetBytes(Settings.JwtSecret);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);

        #endregion

        #region Generate Claims

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
            new(ClaimTypes.Email, usuario.Email.Endereco),
            new(ClaimTypes.Name, usuario.Nome),
            new(ClaimTypes.Role, usuario.UsuarioFuncao.GetDescription()),
            new(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString())
        };

        #endregion

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);
        return encryptedToken;
    }
}