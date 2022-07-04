using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models.Base;
using Vip.SGI.Domain.ValueObjects;
using Vip.SGI.Shared.Constants;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.Models;

public class Pessoa : EntidadeBase
{
    #region Propriedades

    public Guid PessoaId { get; private set; }
    public TipoPessoa TipoPessoa { get; private set; }
    public string Nome { get; private set; }
    public string Fantasia { get; private set; }
    public CpfCnpj CpfCnpj { get; private set; }
    public string RgIe { get; private set; }
    public NaturezaJuridica NaturezaJuridica { get; private set; }
    public RegimeTributario RegimeTributario { get; private set; }
    public Endereco Endereco { get; private set; }
    public int CidadeId { get; private set; }
    public Telefone Telefone { get; private set; }
    public Telefone Celular { get; private set; }
    public Telefone TelefoneContato { get; private set; }
    public string NomeContato { get; private set; }
    public Email Email { get; private set; }
    public Email EmailCobranca { get; private set; }
    public string Observacao { get; private set; }
    public string HostErp { get; private set; }
    public int PortaErp { get; private set; }
    public string DatabaseErp { get; private set; }
    public string UsuarioErp { get; private set; }
    public string SenhaErp { get; private set; }
    public bool Inativo { get; private set; }

    public virtual Cidade Cidade { get; private set; }

    #endregion

    #region Construtores

    public Pessoa(Guid pessoaId, TipoPessoa tipoPessoa, string nome, string fantasia, CpfCnpj cpfCnpj, string rgIe, NaturezaJuridica naturezaJuridica, RegimeTributario regimeTributario, Endereco endereco, int cidadeId, Telefone telefone, Telefone celular, Telefone telefoneContato,
        string nomeContato, Email email, Email emailCobranca, string observacao, string hostErp, int portaErp, string databaseErp, string usuarioErp, string senhaErp, bool inativo = false)
    {
        PessoaId = pessoaId;
        TipoPessoa = tipoPessoa;
        Nome = nome.RemoveAccent().RemoveSpecialChars().TrimVip();
        Fantasia = fantasia.TrimVip();
        CpfCnpj = cpfCnpj;
        RgIe = rgIe;
        NaturezaJuridica = naturezaJuridica;
        RegimeTributario = regimeTributario;
        Endereco = endereco;
        CidadeId = cidadeId;
        Telefone = telefone;
        Celular = celular;
        TelefoneContato = telefoneContato;
        NomeContato = nomeContato;
        Email = email;
        EmailCobranca = emailCobranca;
        Observacao = observacao;
        HostErp = hostErp.TrimVip();
        PortaErp = portaErp;
        DatabaseErp = databaseErp;
        UsuarioErp = usuarioErp;
        SenhaErp = senhaErp;
        Inativo = inativo;

        Validar();
    }

    protected Pessoa() { }

    #endregion

    #region Métodos Públicos

    public void Novo()
    {
        PessoaId = Guid.NewGuid();
        Validar();
    }

    #endregion

    #region Métodos Privados

    private void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .AreNotEquals(PessoaId, Guid.Empty, this.FullName(x => x.PessoaId), ValidMessages.CampoVazio(this.Name(x => x.PessoaId)))
            .IsNotNullOrEmpty(Nome, this.FullName(x => x.Nome), ValidMessages.CampoNuloVazio("Nome"))
            .HasMaxLen(Nome, NomeMaxLength, this.FullName(x => x.Nome), ValidMessages.CampoTamanhoMaximo("Nome", NomeMaxLength))
            .IsNotNullOrEmpty(Fantasia, this.FullName(x => x.Fantasia), ValidMessages.CampoNuloVazio("Fantasia"))
            .HasMaxLen(Fantasia, FantasiaMaxLength, this.FullName(x => x.Fantasia), ValidMessages.CampoTamanhoMaximo("Fantasia", FantasiaMaxLength))
            .HasMaxLen(RgIe, RgIeMaxLength, this.FullName(x => x.RgIe), ValidMessages.CampoTamanhoMaximo("RG ou IE", RgIeMaxLength))
            .IsGreaterThan(CidadeId, 0, this.FullName(x => x.CidadeId), ValidMessages.CampoDiferenteZero(this.Name(x => x.CidadeId)))
            .HasMaxLen(NomeContato, NomeContatoMaxLength, this.FullName(x => x.NomeContato), ValidMessages.CampoTamanhoMaximo("Nome do Contato", NomeContatoMaxLength))
            .HasMaxLen(Observacao, ObservacaoMaxLength, this.FullName(x => x.Observacao), ValidMessages.CampoTamanhoMaximo("Observação", ObservacaoMaxLength))
            .HasMaxLen(HostErp, HostErpMaxLength, this.FullName(x => x.HostErp), ValidMessages.CampoTamanhoMaximo("Host do ERP", HostErpMaxLength))
            .HasMaxLen(DatabaseErp, DatabaseErpMaxLength, this.FullName(x => x.DatabaseErp), ValidMessages.CampoTamanhoMaximo("Database do ERP", DatabaseErpMaxLength))
            .HasMaxLen(UsuarioErp, UsuarioErpMaxLength, this.FullName(x => x.UsuarioErp), ValidMessages.CampoTamanhoMaximo("Usuário do ERP", UsuarioErpMaxLength))
            .HasMaxLen(SenhaErp, SenhaErpMaxLength, this.FullName(x => x.SenhaErp), ValidMessages.CampoTamanhoMaximo("Senha do ERP", SenhaErpMaxLength))
        );

        AddNotifications(CpfCnpj.Notifications);
        AddNotifications(Endereco.Notifications);
        AddNotifications(Telefone.Notifications);
        AddNotifications(Celular.Notifications);
        AddNotifications(TelefoneContato.Notifications);
        AddNotifications(Email.Notifications);
        AddNotifications(EmailCobranca.Notifications);
    }




    #endregion

    #region Constantes

    public const int NomeMaxLength = 100;
    public const int FantasiaMaxLength = 100;
    public const int RgIeMaxLength = 20;
    public const int NomeContatoMaxLength = 50;
    public const int ObservacaoMaxLength = 500;
    public const int HostErpMaxLength = 50;
    public const int DatabaseErpMaxLength = 20;
    public const int UsuarioErpMaxLength = 20;
    public const int SenhaErpMaxLength = 100;

    #endregion
}