using Vip.SGI.Domain.Tests.Builders;
using Vip.SGI.Domain.ValueObjects;
using Xunit;

namespace Vip.SGI.Domain.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Email_Validar_DeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var endereco = new string('a', Email.EnderecoMaxLength) + "@vip.com";

        // Act
        var email = EmailBuilder.Novo().ComEndereco(endereco).Build();

        // Assert
        Assert.True(email.Invalid);
        Assert.Equal($"O campo E-mail deve ter no máximo {Email.EnderecoMaxLength} caracteres", email.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void Email_Validar_OEnderecoInformadoDeveSerUmEmailValidoOuEmBranco()
    {
        // Arrange
        const string endereco = "email";

        // Act
        var email1 = new Email(endereco);
        var email2 = new Email(string.Empty);

        // Assert
        Assert.True(email1.Invalid);
        Assert.Equal("O endereço de E-mail informado é inválido", email1.Notifications.FirstOrDefault()?.Message);
        Assert.True(email2.Valid);
    }

    [Fact]
    public void Email_Objeto_DeveRetornarUmObjetoEmailValido()
    {
        // Arrange
        var email = EmailBuilder.Novo().Build();

        // Assert
        Assert.IsAssignableFrom<Email>(email);
        Assert.True(email.Valid);
    }

    [Fact]
    public void Email_SeValido_DeveRetornarSeEmailValidoOuNao()
    {
        // Arrange
        const string emailValido = "teste@teste.com";
        const string emailInvalido = "teste@teste";

        // Act
        var retornoValido = Email.Validar(emailValido);
        var retornoInvalido = Email.Validar(emailInvalido);

        // Assert
        Assert.True(retornoValido);
        Assert.False(retornoInvalido);
    }
}