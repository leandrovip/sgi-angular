using Vip.SGI.Domain.ValueObjects;
using Xunit;

namespace Vip.SGI.Domain.Tests.ValueObjects;

public class CepTests
{
    [Fact]
    public void Cep_Formatar_DeveRetornarVazioCasoCepForEmbrancoOuNulo()
    {
        // Arrange
        const string valor2 = "";

        // Act
        var resultado1 = Cep.Formatar(null);
        var resultado2 = Cep.Formatar(valor2);

        // Assert
        Assert.Equal("", resultado1);
        Assert.Equal("", resultado2);
    }

    [Theory]
    [InlineData("14740000", "14740-000")]
    [InlineData("147400000", "147400000")]
    public void Cep_Formatar_DeveRetornarCepFormatadoCorretamente(string cep, string cepFormatado)
    {
        // Assert
        Assert.Equal(cepFormatado, Cep.Formatar(cep));
    }

    [Theory]
    [InlineData("14740000")]
    [InlineData("14740-000")]
    public void Cep_Validar_DeveRetornarTrueCasoCepForValido(string cepValido)
    {
        // Assert
        Assert.True(Cep.Validar(cepValido));
    }

    [Theory]
    [InlineData("1474000-")]
    [InlineData("14740- 00")]
    public void Cep_Validar_DeveRetornarFalseCasoCepNaoForValido(string cepInvalido)
    {
        // Assert
        Assert.False(Cep.Validar(cepInvalido));
    }

    [Fact]
    public void Cep_Validar_OCepDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var numero = new string('0', Cep.NumeroMaxLength + 1);

        // Act
        var cep = new Cep(numero);

        // Assert
        Assert.True(cep.Invalid);
        Assert.Equal($"O campo Cep deve ter no máximo {Cep.NumeroMaxLength} caracteres", cep.Notifications.FirstOrDefault()?.Message);
    }
}