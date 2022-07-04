using ExpectedObjects;
using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.Tests.Builders;
using Vip.SGI.Domain.ValueObjects;
using Vip.SGI.Shared.Constants;
using Vip.Tests.Templates;
using Xunit;

namespace Vip.SGI.Domain.Tests.Models;

public class UsuarioTests
{
    [Fact]
    public void UsuarioTests_Construtor_DeveCriarUsuarioValido()
    {
        // Arrange
        var usuarioEsperado = new
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "NOME USUARIO",
            Email = new Email("teste@teste.com"),
            Senha = "senhaTeste",
            UsuarioFuncao = UsuarioFuncao.Administrador
        };

        // Act
        var usuario = new Usuario(usuarioEsperado.UsuarioId, usuarioEsperado.Nome, usuarioEsperado.Email.Endereco, usuarioEsperado.Senha, usuarioEsperado.UsuarioFuncao);

        // Assert
        usuarioEsperado.ToExpectedObject().ShouldMatch(usuario);
    }

    [Fact]
    public void UsuarioTests_Validar_CampoUsuarioIdNaoDeveSerVazio()
    {
        // Act
        var usuario = UsuarioBuilder.Novo().ComUsuarioId(Guid.Empty).Build();

        // Assert
        Assert.False(usuario.Valid);
        Assert.Equal(ValidMessages.CampoVazio("UsuarioId"), usuario.Notifications.FirstOrDefault()?.Message);
    }

    [Theory]
    [ClassData(typeof(TestNotNullOrEmptyString))]
    public void UsuarioTests_Validar_CampoNomeNaoDeveSerNuloOuVazio(string nomeInvalido)
    {
        // Act
        var usuario = UsuarioBuilder.Novo().ComNome(nomeInvalido).Build();

        // Assert
        Assert.False(usuario.Valid);
        Assert.Equal(ValidMessages.CampoNuloVazio("Nome"), usuario.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void UsuarioTests_Validar_CampoNomeDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var nomeInvalido = new string('a', Usuario.NomeMaxLength + 1);

        // Act
        var usuario = UsuarioBuilder.Novo().ComNome(nomeInvalido).Build();

        // Assert
        Assert.False(usuario.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Nome", Usuario.NomeMaxLength), usuario.Notifications.FirstOrDefault()?.Message);
    }

    [Theory]
    [ClassData(typeof(TestNotNullOrEmptyString))]
    public void UsuarioTests_Validar_CampoSenhaNaoDeveSerNuloOuVazio(string senhaInvalida)
    {
        // Act
        var usuario = UsuarioBuilder.Novo().ComSenha(senhaInvalida).Build();

        // Assert
        Assert.False(usuario.Valid);
        Assert.Equal(ValidMessages.CampoNuloVazio("Senha"), usuario.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void UsuarioTests_Validar_CampoSenhaDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var senhaInvalida = new string('a', Usuario.SenhaMaxLength + 1);

        // Act
        var usuario = UsuarioBuilder.Novo().ComSenha(senhaInvalida).Build();

        // Assert
        Assert.False(usuario.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Senha", Usuario.SenhaMaxLength), usuario.Notifications.FirstOrDefault()?.Message);
    }
}