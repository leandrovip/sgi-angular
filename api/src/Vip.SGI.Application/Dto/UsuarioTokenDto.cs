namespace Vip.SGI.Application.Dto;

public class UsuarioTokenDto
{
    #region Propriedades

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }

    #endregion

    #region Construtor

    public UsuarioTokenDto(string nome, string email, string token)
    {
        Nome = nome;
        Email = email;
        Token = token;
    }

    protected UsuarioTokenDto() { }

    #endregion
}