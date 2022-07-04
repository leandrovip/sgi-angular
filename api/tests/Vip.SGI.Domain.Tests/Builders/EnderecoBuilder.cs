using Vip.SGI.Domain.ValueObjects;
using Vip.Tests.Builder;

namespace Vip.SGI.Domain.Tests.Builders;

public class EnderecoBuilder : BuilderBase
{
    #region Propriedades

    private string _logradouro;
    private string _numero;
    private string _complemento;
    private string _bairro;
    private readonly Cep _cep = new("14780000");

    #endregion

    #region Construtores

    private EnderecoBuilder()
    {
        _logradouro = faker.Address.StreetAddress();
        _numero = faker.Address.BuildingNumber();
        _complemento = faker.Address.CardinalDirection();
        _bairro = faker.Random.AlphaNumeric(Endereco.BairroMaxLength);
    }

    #endregion

    #region Métodos

    public EnderecoBuilder ComLogradouro(string logradouro)
    {
        _logradouro = logradouro;
        return this;
    }

    public EnderecoBuilder ComNumero(string numero)
    {
        _numero = numero;
        return this;
    }

    public EnderecoBuilder ComComplemento(string complemento)
    {
        _complemento = complemento;
        return this;
    }

    public EnderecoBuilder ComBairro(string bairro)
    {
        _bairro = bairro;
        return this;
    }

    public Endereco Build()
    {
        return new(_logradouro, _numero, _complemento, _bairro, _cep);
    }

    #endregion

    #region Métodos Estáticos

    public static EnderecoBuilder Novo() => new();

    #endregion
}