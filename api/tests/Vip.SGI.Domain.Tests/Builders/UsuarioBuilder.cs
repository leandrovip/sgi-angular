using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.ValueObjects;
using Vip.Tests.Builder;

namespace Vip.SGI.Domain.Tests.Builders;

public class UsuarioBuilder : BuilderBase
{
    #region Propriedades

    private Guid _usuarioId;
    private string _nome;
    private Email _email;
    private string _senha;
    private readonly UsuarioFuncao _usuarioFuncao;

    #endregion

    #region Construtores

    private UsuarioBuilder()
    {
        _usuarioId = faker.Random.Guid();
        _nome = faker.Random.String2(Usuario.NomeMaxLength);
        _email = EmailBuilder.Novo().Build();
        _senha = faker.Random.String(Usuario.SenhaMaxLength);
        _usuarioFuncao = faker.PickRandom<UsuarioFuncao>();
    }

    #endregion

    #region Métodos Públicos

    public UsuarioBuilder ComUsuarioId(Guid usuarioId)
    {
        _usuarioId = usuarioId;
        return this;
    }

    public UsuarioBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public UsuarioBuilder ComEmail(string email)
    {
        _email = new Email(email);
        return this;
    }

    public UsuarioBuilder ComSenha(string senha)
    {
        _senha = senha;
        return this;
    }

    public Usuario Build()
    {
        return new(_usuarioId, _nome, _email.Endereco, _senha, _usuarioFuncao);
    }

    #endregion

    #region Métodos Estáticos

    public static UsuarioBuilder Novo() => new();

    #endregion
}