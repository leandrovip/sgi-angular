using Vip.SGI.Domain.ValueObjects;
using Xunit;

namespace Vip.SGI.Domain.Tests.ValueObjects;

public class CpfCnpjTests
{
    [Theory]
    [InlineData("12332134000199")]
    [InlineData("12.332.134/0001-99")]
    public void CpfCnpj_ValidarNumero_DeveRetornarTrueSeNumneroInformadoForCnpjValido(string numero)
    {
        // Arrange
        var retorno = CpfCnpj.ValidarNumero(numero);

        // Assert
        Assert.True(retorno);
    }

    [Theory]
    [InlineData("12332134000198")]
    [InlineData("12.332.134/0001-98")]
    public void CpfCnpj_ValidarNumero_DeveRetornarFalseQuandoCnpjInformadoForInvalido(string numero)
    {
        // Arrange
        var retorno = CpfCnpj.ValidarNumero(numero);

        // Assert
        Assert.False(retorno);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CpfCnpj_ValidarNumero_DeveRetornarFalseQuandoCnpjInformadoForEmBrancoOuNulo(string numero)
    {
        // Arrange
        var retorno = CpfCnpj.ValidarNumero(numero);

        // Assert
        Assert.False(retorno);
    }

    [Theory]
    [InlineData("12.332.134/0001-99", "12332134000199")]
    public void CpfCnpj_FormatarNumero_DeveRetornarOCnpjFormatadoCorretamente(string cnpjFormatado, string cnpj)
    {
        // Assert
        Assert.Equal(cnpjFormatado, CpfCnpj.FormatarNumero(cnpj));
    }

    [Theory]
    [InlineData("123456ABCD-", "123456ABCD-")]
    public void CpfCnpj_FormatarNumero_SeNaoForUmCnpjValidoDeveRetonarMesmoValorInformado(string cnpjIncorreto, string cnpjEsperado)
    {
        // Assert
        Assert.Equal(cnpjEsperado, CpfCnpj.FormatarNumero(cnpjIncorreto));
    }

    [Theory]
    [InlineData("33974986863")]
    [InlineData("182.342.240-34")]
    [InlineData("030.319.850-87")]
    public void CpfCnpj_ValidarNumero_DeveRetornarTrueSeNumeroInformadoForCpfValido(string cpfValido)
    {
        Assert.True(CpfCnpj.ValidarNumero(cpfValido));
    }

    [Theory]
    [InlineData("33974986865")]
    [InlineData("33974986862")]
    [InlineData("339749868")]
    public void CpfCnpj_ValidarNumero_DeveRetornarFalseSeNumeroInformadoForCpfInvalido(string cpfInvalido)
    {
        Assert.False(CpfCnpj.ValidarNumero(cpfInvalido));
    }

    [Theory]
    [InlineData("33974986863", "339.749.868-63")]
    public void CpfCnpj_FormatarNumero_DeveRetornarUmCpfFormatadoCorretamenteSeForValido(string cpf, string cpfFormatado)
    {
        Assert.Equal(cpfFormatado, CpfCnpj.FormatarNumero(cpf));
    }

    [Fact]
    public void CepCnpj_Validar_OCpfOuCnpjDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var documento = new string('0', CpfCnpj.NumeroMaxLength + 1);

        // Act
        var cpfcnpj = new CpfCnpj(documento);

        // Assert
        Assert.True(cpfcnpj.Invalid);
        Assert.Equal($"O campo Cpf ou Cnpj deve ter no máximo {CpfCnpj.NumeroMaxLength} caracteres", cpfcnpj.Notifications.FirstOrDefault()?.Message);
    }

    [Theory]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    [InlineData("00000000000000")]
    [InlineData("11111111111111")]
    public void CpfCnpj_Validar_DeveRetornarFalseCasoCpfOuCnpjTiverTodosOsCaracteresIguais(string documentoInvalido)
    {
        // Act
        var retorno = CpfCnpj.ValidarNumero(documentoInvalido);

        // Assert
        Assert.False(retorno);
    }

    [Fact]
    public void CpfCnpj_NumeroFormatado_DeveRetornarNumeroDoDocumentoFormatado()
    {
        // Act
        var retorno = new CpfCnpj("12332134000199");

        // Assert
        Assert.Equal("12.332.134/0001-99", retorno.NumeroFormatado);
    }
}