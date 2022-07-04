using Vip.SGI.Domain.Tests.Builders;
using Vip.SGI.Domain.ValueObjects;
using Xunit;

namespace Vip.SGI.Domain.Tests.ValueObjects;

public class TelefoneTests
{
    [Fact]
    public void Telefone_Formatar_DeveRetornarONumeroFormatadoCasoTenha8Digitos()
    {
        // Arrange
        const string numeroSemFormatacao = "33924084";
        const string numeroFormatado = "3392-4084";

        // Act
        var result = Telefone.Formatar(numeroSemFormatacao);
        var result2 = Telefone.Formatar(numeroFormatado);

        // Assert
        Assert.Equal(numeroFormatado, result);
        Assert.Equal(numeroFormatado, result2);
    }

    [Fact]
    public void Telefone_Formatar_DeveRetornarONumeroFormatadoCasoTenha9Digitos()
    {
        // Arrange
        const string numeroSemFormatacao = "991765035";
        const string numeroFormatado = "99176-5035";

        // Act
        var result = Telefone.Formatar(numeroSemFormatacao);
        var result2 = Telefone.Formatar(numeroFormatado);

        // Assert
        Assert.Equal(numeroFormatado, result);
        Assert.Equal(numeroFormatado, result2);
    }

    [Fact]
    public void Telefone_Formatar_DeveRetornarONumeroFormatadoCasoTenha10Digitos()
    {
        // Arrange
        const string numeroSemFormatacao = "1733924084";
        const string numeroFormatado = "(17) 3392-4084";

        // Act
        var result = Telefone.Formatar(numeroSemFormatacao);
        var result2 = Telefone.Formatar(numeroFormatado);

        // Assert
        Assert.Equal(numeroFormatado, result);
        Assert.Equal(numeroFormatado, result2);
    }

    [Fact]
    public void Telefone_Formatar_DeveRetornarONumeroFormatadoCasoTenha11Digitos()
    {
        // Arrange
        const string numeroSemFormatacao = "17991765035";
        const string numeroFormatado = "(17) 99176-5035";

        // Act
        var result = Telefone.Formatar(numeroSemFormatacao);
        var result2 = Telefone.Formatar(numeroFormatado);

        // Assert
        Assert.Equal(numeroFormatado, result);
        Assert.Equal(numeroFormatado, result2);
    }

    [Fact]
    public void Telefone_Formatar_DeveRetornarEmBrancoCasoNaoTenhaNumeros()
    {
        // Arrange
        const string valorSemNumeros = "()-";

        // Act
        var result = Telefone.Formatar(valorSemNumeros);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Telefone_Validar_DeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var numero = new string('0', Telefone.NumeroMaxLength + 1);

        // Act
        var telefone = TelefoneBuilder.Novo().ComNumero(numero).Build();

        // Assert
        Assert.True(telefone.Invalid);
        Assert.Equal($"O campo Telefone deve ter no máximo {Telefone.NumeroMaxLength} caracteres", telefone.Notifications.FirstOrDefault()?.Message);
    }
}