using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.ValueObjects;
using Vip.Tests.Builder;

namespace Vip.SGI.Domain.Tests.Builders;

public class PessoaBuilder : BuilderBase
{
    #region Propriedades

    private Guid _pessoaId;
    private TipoPessoa _tipoPessoa;
    private string _nome;
    private string _fantasia;
    private readonly CpfCnpj _cpfCnpj;
    private string _rgIe;
    private readonly NaturezaJuridica _naturezaJuridica;
    private readonly RegimeTributario _regimeTributario;
    private readonly Endereco _endereco;
    private int _cidadeId;
    private readonly Telefone _telefone;
    private readonly Telefone _celular;
    private readonly Telefone _telefoneContato;
    private string _nomeContato;
    private readonly Email _email;
    private readonly Email _emailCobranca;
    private string _observacao;
    private string _hostErp;
    private readonly int _portaErp;
    private string _databaseErp;
    private string _usuarioErp;
    private string _senhaErp;

    #endregion

    #region Construtores

    private PessoaBuilder()
    {
        _pessoaId = faker.Random.Guid();
        _tipoPessoa = faker.PickRandom<TipoPessoa>();
        _nome = faker.Random.String2(Pessoa.NomeMaxLength);
        _fantasia = faker.Random.String2(Pessoa.FantasiaMaxLength);
        _cpfCnpj = new CpfCnpj("33974986863");
        _rgIe = faker.Random.String2(Pessoa.RgIeMaxLength);
        _naturezaJuridica = faker.PickRandom<NaturezaJuridica>();
        _regimeTributario = faker.PickRandom<RegimeTributario>();
        _endereco = EnderecoBuilder.Novo().Build();
        _cidadeId = faker.Random.Int(1);
        _telefone = TelefoneBuilder.Novo().Build();
        _celular = TelefoneBuilder.Novo().Build();
        _telefoneContato = TelefoneBuilder.Novo().Build();
        _nomeContato = faker.Random.String2(Pessoa.NomeContatoMaxLength);
        _email = EmailBuilder.Novo().Build();
        _emailCobranca = EmailBuilder.Novo().Build();
        _observacao = faker.Random.String2(Pessoa.ObservacaoMaxLength);
        _hostErp = faker.Random.String2(Pessoa.HostErpMaxLength);
        _portaErp = 1433;
        _databaseErp = faker.Random.String2(Pessoa.DatabaseErpMaxLength);
        _usuarioErp = faker.Random.String2(Pessoa.UsuarioErpMaxLength);
        _senhaErp = faker.Random.String2(Pessoa.SenhaErpMaxLength);
    }

    #endregion

    #region Métodos Públicos

    public PessoaBuilder ComPessoaId(Guid pessoaId)
    {
        _pessoaId = pessoaId;
        return this;
    }

    public PessoaBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public PessoaBuilder ComFantasia(string fantasia)
    {
        _fantasia = fantasia;
        return this;
    }

    public PessoaBuilder ComRgIe(string rgIe)
    {
        _rgIe = rgIe;
        return this;
    }

    public PessoaBuilder ComCidadeId(int cidadeId)
    {
        _cidadeId = cidadeId;
        return this;
    }

    public PessoaBuilder ComNomeContato(string nomeContato)
    {
        _nomeContato = nomeContato;
        return this;
    }

    public PessoaBuilder ComObservacao(string observacao)
    {
        _observacao = observacao;
        return this;
    }

    public PessoaBuilder ComHostErp(string hostErp)
    {
        _hostErp = hostErp;
        return this;
    }

    public PessoaBuilder ComDatabaseErp(string databaseErp)
    {
        _databaseErp = databaseErp;
        return this;
    }

    public PessoaBuilder ComUsuarioErp(string usuarioErp)
    {
        _usuarioErp = usuarioErp;
        return this;
    }

    public PessoaBuilder ComSenhaErp(string senhaErp)
    {
        _senhaErp = senhaErp;
        return this;
    }

    public Pessoa Build()
    {
        return new Pessoa(_pessoaId, _tipoPessoa, _nome, _fantasia, _cpfCnpj, _rgIe, _naturezaJuridica, _regimeTributario, _endereco, _cidadeId, _telefone, _celular, _telefoneContato, _nomeContato, _email, _emailCobranca, _observacao, _hostErp, _portaErp, _databaseErp, _usuarioErp, _senhaErp);
    }

    #endregion

    #region Métodos Estáticos

    public static PessoaBuilder Novo() => new();

    #endregion
}