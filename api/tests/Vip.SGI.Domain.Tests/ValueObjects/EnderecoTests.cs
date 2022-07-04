using Vip.SGI.Domain.Tests.Builders;
using Vip.SGI.Domain.ValueObjects;
using Xunit;

namespace Vip.SGI.Domain.Tests.ValueObjects;

public class EnderecoTests
{
    [Fact]
    public void Endereco_Validar_CampoLogradouroDeveAceitarNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var logradouro = new string('a', Endereco.LogradouroMaxLength + 1);

        // Act
        var endereco = EnderecoBuilder.Novo().ComLogradouro(logradouro).Build();

        // Assert
        Assert.True(endereco.Invalid);
        Assert.Equal($"O campo Logradouro deve ter no máximo {Endereco.LogradouroMaxLength} caracteres", endereco.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void Endereco_Validar_CampoNumeroDeveAceitarNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var numero = new string('0', Endereco.NumeroMaxLength + 1);

        // Act
        var endereco = EnderecoBuilder.Novo().ComNumero(numero).Build();

        // Assert
        Assert.True(endereco.Invalid);
        Assert.Equal($"O campo Número deve ter no máximo {Endereco.NumeroMaxLength} caracteres", endereco.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void Endereco_Validar_OCampoComplementoDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var complemento = new string('a', Endereco.ComplementoMaxLength + 1);

        // Act
        var endereco = EnderecoBuilder.Novo().ComComplemento(complemento).Build();

        // Assert
        Assert.True(endereco.Invalid);
        Assert.Equal($"O campo Complemento deve ter no máximo {Endereco.ComplementoMaxLength} caracteres", endereco.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void Endereco_Validar_OCampoBairroDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var bairro = new string('a', Endereco.BairroMaxLength + 1);

        // Act
        var endereco = EnderecoBuilder.Novo().ComBairro(bairro).Build();

        // Assert
        Assert.True(endereco.Invalid);
        Assert.Equal($"O campo Bairro deve ter no máximo {Endereco.BairroMaxLength} caracteres", endereco.Notifications.FirstOrDefault()?.Message);
    }
}