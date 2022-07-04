using Vip.Validator.Notifications;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.ValueObjects;

public class CpfCnpj : Notifiable
{
    #region Propriedades

    public string Numero { get; private set; }

    public string NumeroFormatado => FormatarNumero(Numero);

    #endregion

    #region Construtores

    public CpfCnpj(string numero)
    {
        Numero = numero.OnlyNumbers();
        Validar();
    }

    protected CpfCnpj() { }

    #endregion

    #region Métodos

    private void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasMaxLen(Numero, NumeroMaxLength, "CpfCnpj.Numero", $"O campo Cpf ou Cnpj deve ter no máximo {NumeroMaxLength} caracteres"));
    }

    public override string ToString()
    {
        return Numero.OnlyNumbers();
    }

    #endregion

    #region Constantes

    public const int NumeroMaxLength = 20;

    #endregion

    #region Métodos Estáticos

    public static bool ValidarNumero(string numero)
    {
        if (numero.IsNullOrEmpty())
            return false;

        numero = numero.OnlyNumbers();
        return numero.Length == 14 ? ValidarCnpj(numero) : ValidarCpf(numero);
    }

    private static bool ValidarCpf(string numero)
    {
        if (numero.Length != 11)
            return false;

        if (numero == new string(numero.ToCharArray()[0], 11))
            return false;

        var digito = numero.Substring(9, 2);

        numero = numero.Substring(0, 9);
        numero += GerarDigito(numero);
        numero += GerarDigito(numero);

        return digito == numero.Substring(9, 2);
    }

    private static bool ValidarCnpj(string numero)
    {
        if (numero == new string(numero.ToCharArray()[0], 14))
            return false;

        var digito = numero.Substring(12, 2);

        numero = numero.Substring(0, 12);
        numero += GerarDigito(numero);
        numero += GerarDigito(numero);

        return digito == numero.Substring(12, 2);
    }

    private static string GerarDigito(string numero)
    {
        var peso = 2;
        var soma = 0;

        var cnpj = numero.Length > 11;

        for (var i = numero.Length - 1; i >= 0; i--)
        {
            soma += peso * Convert.ToInt32(numero[i].ToString());
            peso++;

            if (cnpj && peso == 10)
                peso = 2;
        }

        var digito = 11 - soma % 11;
        if (digito > 9)
            digito = 0;

        return digito.ToString();
    }

    public static string FormatarNumero(string numero)
    {
        if (numero.IsNullOrEmpty())
            return string.Empty;

        var tempNumero = numero.OnlyNumbers();
        switch (tempNumero.Length)
        {
            case 11:
                return Convert.ToInt64(tempNumero).ToString(@"000\.000\.000\-00");
            case 14:
                return Convert.ToInt64(tempNumero).ToString(@"00\.000\.000\/0000\-00");
            default:
                return numero;
        }
    }

    #endregion
}