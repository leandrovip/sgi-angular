namespace Vip.SGI.Application.Dto;

public class UsuarioTokenDto
{
    #region Propriedades

    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }

    #endregion

    #region Construtor

    public UsuarioTokenDto(Guid usuarioId, string nome, string email, string token)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
        AccessToken = token;
    }

    protected UsuarioTokenDto() { }

    #endregion
}