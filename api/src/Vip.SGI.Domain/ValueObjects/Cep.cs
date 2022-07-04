using Vip.Validator.Notifications;
using Vip.Validator.Validations;

namespace Vip.SGI.Domain.ValueObjects;

public class Cep : Notifiable
{
    #region Propriedades

    public string Numero { get; private set; }
    public string NumeroFormatado => Formatar(Numero);

    #endregion

    #region Construtores

    public Cep(string numero)
    {
        Numero = numero.OnlyNumbers();
        Validar();
    }

    protected Cep() { }

    #endregion

    #region Métodos

    private void Validar()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasMaxLen(Numero, NumeroMaxLength, "Cep.Numero", $"O campo Cep deve ter no máximo {NumeroMaxLength} caracteres"));
    }

    public override string ToString()
    {
        return Numero;
    }

    public static string Formatar(string cep)
    {
        if (cep.IsNullOrEmpty())
            return string.Empty;

        cep = cep.OnlyNumbers();
        return cep.Length != 8 ? cep : Convert.ToInt64(cep).ToString(@"00000\-000");
    }

    #endregion

    #region Constantes

    public const int NumeroMaxLength = 10;

    #endregion

    #region Métodos Estáticos

    public static bool Validar(string cep)
    {
        cep = cep.OnlyNumbers();
        return cep.Length == 8;
    }

    #endregion
}