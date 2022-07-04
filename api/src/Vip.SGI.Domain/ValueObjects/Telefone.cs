using Vip.Validator.Notifications;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.ValueObjects;

public class Telefone : Notifiable
{
    #region Propriedades

    public string Numero { get; private set; }
    public string NumeroFormatado => Formatar(Numero);

    #endregion

    #region Construtores

    public Telefone(string numero)
    {
        Numero = numero.OnlyNumbers();
        Validar();
    }

    protected Telefone() { }

    #endregion

    #region Métodos

    public void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasMaxLen(Numero, NumeroMaxLength, "Telefone.Numero", $"O campo Telefone deve ter no máximo {NumeroMaxLength} caracteres"));
    }

    public override string ToString() => Numero;

    #endregion

    #region Constantes

    public const int NumeroMaxLength = 20;

    #endregion

    #region Métodos Estáticos

    public static string Formatar(string numero)
    {
        var tempNumero = numero.OnlyNumbers();

        if (tempNumero.IsNullOrEmpty())
            return string.Empty;

        switch (tempNumero.Length)
        {
            case 8:
                return Convert.ToInt64(tempNumero).ToString("####-####");
            case 9:
                return Convert.ToInt64(tempNumero).ToString("#####-####");
            case 10:
                return Convert.ToInt64(tempNumero).ToString("(00) ####-####");
            case 11:
                return Convert.ToInt64(tempNumero).ToString("(00) #####-####");
            default:
                return tempNumero;
        }
    }

    #endregion
}