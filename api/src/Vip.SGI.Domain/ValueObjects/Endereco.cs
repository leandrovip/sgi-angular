using Vip.Validator.Notifications;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.ValueObjects;

public class Endereco : Notifiable
{
    #region Propriedades

    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public Cep Cep { get; private set; }

    #endregion

    #region Construtores

    public Endereco(string logradouro, string numero, string complemento, string bairro, Cep cep)
    {
        Logradouro = logradouro.TrimVip().RemoveAccent();
        Numero = numero.TrimVip();
        Complemento = complemento.TrimVip().RemoveAccent();
        Bairro = bairro.TrimVip().RemoveAccent();
        Cep = cep;

        Validar();
    }

    protected Endereco() { }

    #endregion

    #region Métodos

    public void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasMaxLen(Logradouro, LogradouroMaxLength, "Endereco.Logradouro", $"O campo Logradouro deve ter no máximo {LogradouroMaxLength} caracteres")
            .HasMaxLen(Numero, NumeroMaxLength, "Endereco.Numero", $"O campo Número deve ter no máximo {NumeroMaxLength} caracteres")
            .HasMaxLen(Complemento, ComplementoMaxLength, "Endereco.Complemento", $"O campo Complemento deve ter no máximo {ComplementoMaxLength} caracteres")
            .HasMaxLen(Bairro, BairroMaxLength, "Endereco.Bairro", $"O campo Bairro deve ter no máximo {BairroMaxLength} caracteres"));

        AddNotifications(Cep.Notifications);
    }

    #endregion

    #region Constantes

    public const int LogradouroMaxLength = 100;
    public const int NumeroMaxLength = 10;
    public const int ComplementoMaxLength = 100;
    public const int BairroMaxLength = 50;

    #endregion
}