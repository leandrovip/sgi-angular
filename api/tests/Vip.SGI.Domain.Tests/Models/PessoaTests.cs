using ExpectedObjects;
using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.Tests.Builders;
using Vip.SGI.Domain.ValueObjects;
using Vip.SGI.Shared.Constants;
using Vip.Tests.Templates;
using Xunit;

namespace Vip.SGI.Domain.Tests.Models;

public class PessoaTests
{
    [Fact]
    public void PessoaTests_Construtor_DeveCriarOjetoPessoaValido()
    {
        // Arrange
        var esperado = new
        {
            PessoaId = Guid.NewGuid(),
            TipoPessoa = TipoPessoa.Cliente,
            Nome = "RAZAO SOCIAL",
            Fantasia = "NOME FANTASIA",
            CpfCnpj = new CpfCnpj("33974986863"),
            RgIe = "012345678",
            NaturezaJuridica = NaturezaJuridica.EmpresarioIndividual,
            RegimeTributario = RegimeTributario.SimplesNacional,
            Endereco = EnderecoBuilder.Novo().Build(),
            CidadeId = 1234567,
            Telefone = TelefoneBuilder.Novo().Build(),
            Celular = TelefoneBuilder.Novo().Build(),
            TelefoneContato = TelefoneBuilder.Novo().Build(),
            NomeContato = "NOME CONTATO",
            Email = EmailBuilder.Novo().Build(),
            EmailCobranca = EmailBuilder.Novo().Build(),
            Observacao = "OBSERVACOES DA PESSOA",
            HostErp = "enderecoHost",
            PortaErp = 1433,
            DatabaseErp = "nomeBancoDados",
            UsuarioErp = "usuarioBancoDados",
            SenhaErp = "senhaErp",
            Inativo = false
        };

        // Act
        var pessoa = new Pessoa(esperado.PessoaId, esperado.TipoPessoa, esperado.Nome, esperado.Fantasia, esperado.CpfCnpj, esperado.RgIe, esperado.NaturezaJuridica, esperado.RegimeTributario, esperado.Endereco, esperado.CidadeId, esperado.Telefone, esperado.Celular, esperado.TelefoneContato,
            esperado.NomeContato, esperado.Email, esperado.EmailCobranca, esperado.Observacao, esperado.HostErp, esperado.PortaErp, esperado.DatabaseErp, esperado.UsuarioErp, esperado.SenhaErp, esperado.Inativo);

        // Assert
        esperado.ToExpectedObject().ShouldMatch(pessoa);
    }

    [Fact]
    public void PessoaTests_Validar_CampoPessoaIdNaoDeveSerVazio()
    {
        // Act
        var cliente = PessoaBuilder.Novo().ComPessoaId(Guid.Empty).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoVazio("PessoaId"), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Theory]
    [ClassData(typeof(TestNotNullOrEmptyString))]
    public void PessoaTests_Validar_CampoNomeNaoDeveSerNuloOuVazio(string nomeInvalido)
    {
        // Act
        var cliente = PessoaBuilder.Novo().ComNome(nomeInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoNuloVazio("Nome"), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoNomeDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var nomeInvalido = new string('a', Pessoa.NomeMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComNome(nomeInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Nome", Pessoa.NomeMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Theory]
    [ClassData(typeof(TestNotNullOrEmptyString))]
    public void PessoaTests_Validar_CampoFantasiaNaoDeveSerNuloOuVazio(string fantasiaInvalido)
    {
        // Act
        var cliente = PessoaBuilder.Novo().ComFantasia(fantasiaInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoNuloVazio("Fantasia"), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoFantasiaDeveTerNoMaximoAQuantidadeDeCarateresEsperada()
    {
        // Arrange
        var fantasiaInvalido = new string('a', Pessoa.FantasiaMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComFantasia(fantasiaInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Fantasia", Pessoa.FantasiaMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoRgIeDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var rgIeInvalido = new string('a', Pessoa.RgIeMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComRgIe(rgIeInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("RG ou IE", Pessoa.RgIeMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoCidadeIdNaoDeveSerZero()
    {
        // Act
        var cliente = PessoaBuilder.Novo().ComCidadeId(0).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoDiferenteZero("CidadeId"), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoNomeContatoDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var nomeContatoInvalido = new string('a', Pessoa.NomeContatoMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComNomeContato(nomeContatoInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Nome do Contato", Pessoa.NomeContatoMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoObservacaoDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var observacaoInvalido = new string('a', Pessoa.ObservacaoMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComObservacao(observacaoInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Observação", Pessoa.ObservacaoMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoHostErpDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var hostErpInvalido = new string('a', Pessoa.HostErpMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComHostErp(hostErpInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Host do ERP", Pessoa.HostErpMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoDatabaseErpDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var databaseErpInvalido = new string('a', Pessoa.DatabaseErpMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComDatabaseErp(databaseErpInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Database do ERP", Pessoa.DatabaseErpMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoUsuarioErpDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var usuarioErpInvalido = new string('a', Pessoa.UsuarioErpMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComUsuarioErp(usuarioErpInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Usuário do ERP", Pessoa.UsuarioErpMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }

    [Fact]
    public void PessoaTests_Validar_CampoSenhaErpDeveTerNoMaximoAQuantidadeDeCaracteresEsperada()
    {
        // Arrange
        var senhaErpInvalido = new string('a', Pessoa.SenhaErpMaxLength + 1);

        // Act
        var cliente = PessoaBuilder.Novo().ComSenhaErp(senhaErpInvalido).Build();

        // Assert
        Assert.False(cliente.Valid);
        Assert.Equal(ValidMessages.CampoTamanhoMaximo("Senha do ERP", Pessoa.SenhaErpMaxLength), cliente.Notifications.FirstOrDefault()?.Message);
    }
}