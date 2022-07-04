using System.Text.RegularExpressions;
using Vip.Validator.Notifications;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.ValueObjects;

public class Email : Notifiable
{
    #region Propriedades

    public string Endereco { get; private set; }

    #endregion

    #region Construtores

    public Email(string endereco)
    {
        Endereco = endereco.TrimVip().ToLower();
        Validar();
    }

    protected Email() { }

    #endregion

    #region Métodos

    private void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsEmailOrEmpty(Endereco, "Email.Endereco", "O endereço de E-mail informado é inválido")
            .HasMaxLen(Endereco, EnderecoMaxLength, "Email.Endereco", $"O campo E-mail deve ter no máximo {EnderecoMaxLength} caracteres"));
    }

    public override string ToString()
    {
        return Endereco;
    }

    #endregion

    #region Métodos Estáticos

    public static bool Validar(string enderecoEmail)
    {
        const string regexEmail = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        return enderecoEmail.IsNotNullOrEmpty() && Regex.IsMatch(enderecoEmail, regexEmail);
    }

    #endregion

    #region Constantes

    public const int EnderecoMaxLength = 256;

    #endregion
}