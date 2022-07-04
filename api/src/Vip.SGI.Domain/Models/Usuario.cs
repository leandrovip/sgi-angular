using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models.Base;
using Vip.SGI.Domain.ValueObjects;
using Vip.SGI.Shared.Constants;
using Vip.SGI.Shared.Extensions;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.Models;

public class Usuario : EntidadeBase
{
    #region Propriedades

    public Guid UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string Senha { get; private set; }
    public UsuarioFuncao UsuarioFuncao { get; private set; }

    #endregion

    #region Construtores

    public Usuario(Guid usuarioId, string nome, string email, string senha, UsuarioFuncao usuarioFuncao)
    {
        UsuarioId = usuarioId;
        Nome = nome.TrimVip();
        Email = new Email(email);
        Senha = senha.TrimVip();
        UsuarioFuncao = usuarioFuncao;

        Validar();
    }

    protected Usuario() { }

    #endregion

    #region Métodos Privados

    public void Criptografar()
    {
        Senha = Senha.Encrypt();
    }

    public void Desencritografar()
    {
        Senha = Senha.Decrypt();
    }

    private void Validar()
    {
        AddNotifications(Email.Notifications);
        AddNotifications(new Contract()
            .Requires()
            .AreNotEquals(UsuarioId, Guid.Empty, this.FullName(x => x.UsuarioId), ValidMessages.CampoVazio(this.Name(x => x.UsuarioId)))
            .IsNotNullOrEmpty(Nome, this.FullName(x => x.Nome), ValidMessages.CampoNuloVazio("Nome"))
            .HasMaxLen(Nome, NomeMaxLength, this.FullName(x => x.Nome), ValidMessages.CampoTamanhoMaximo("Nome", NomeMaxLength))
            .IsNotNullOrEmpty(Senha, this.FullName(x => x.Senha), ValidMessages.CampoNuloVazio("Senha"))
            .HasMaxLen(Senha, SenhaMaxLength, this.FullName(x => x.Senha), ValidMessages.CampoTamanhoMaximo("Senha", SenhaMaxLength))
        );
    }

    #endregion

    #region Constantes

    public const int NomeMaxLength = 50;
    public const int SenhaMaxLength = 200;

    #endregion
}