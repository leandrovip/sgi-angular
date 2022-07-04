using Vip.SGI.Domain.ValueObjects;
using Vip.Tests.Builder;

namespace Vip.SGI.Domain.Tests.Builders;

public class EmailBuilder : BuilderBase
{
    #region Propriedades

    private string _endereco;

    #endregion

    #region Construtores

    private EmailBuilder()
    {
        _endereco = faker.Internet.Email();
    }

    #endregion

    #region Métodos

    public EmailBuilder ComEndereco(string endereco)
    {
        _endereco = endereco;
        return this;
    }

    public Email Build()
    {
        return new Email(_endereco);
    }

    #endregion

    #region Método Estático

    public static EmailBuilder Novo()
    {
        return new EmailBuilder();
    }

    #endregion
}