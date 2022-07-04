namespace Vip.SGI.Application.Dto;

public class UsuarioDto
{
    #region Propriedades

    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string UsuarioFuncao { get; set; }

    #endregion

    #region Construtores

    public UsuarioDto(Guid usuarioId, string nome, string email, string senha, string usuarioFuncao)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
        Senha = senha;
        UsuarioFuncao = usuarioFuncao;
    }

    protected UsuarioDto() { }

    #endregion
}