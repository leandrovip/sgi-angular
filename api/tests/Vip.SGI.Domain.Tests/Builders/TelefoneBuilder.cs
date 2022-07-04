using Vip.SGI.Domain.ValueObjects;
using Vip.Tests.Builder;

namespace Vip.SGI.Domain.Tests.Builders;

public class TelefoneBuilder : BuilderBase
{
    #region Propriedades

    private string _numero;

    #endregion

    #region Construtores

    private TelefoneBuilder()
    {
        _numero = faker.Phone.PhoneNumber();
    }

    #endregion

    #region Métodos

    public TelefoneBuilder ComNumero(string numero)
    {
        _numero = numero;
        return this;
    }

    public Telefone Build() => new(_numero);

    #endregion

    #region Métodos Estáticos

    public static TelefoneBuilder Novo() => new();

    #endregion
}