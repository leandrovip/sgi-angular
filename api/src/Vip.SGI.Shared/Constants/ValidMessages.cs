namespace Vip.SGI.Shared.Constants;

public static class ValidMessages
{
    public static string CampoVazio(string nomeCampo) => $"O campo {nomeCampo} não pode ser vazio";
    public static string CampoNuloVazio(string nomeCampo) => $"O campo {nomeCampo} não pode ser nulo ou vazio";
    public static string CampoTamanhoMaximo(string nomeCampo, int tamanho) => $"O campo {nomeCampo} deve ter no máximo {tamanho} caracteres";
    public static string CampoNegativo(string nomeCampo) => $"O campo {nomeCampo} não pode ser negativo";
    public static string CampoMaiorZero(string nomeCampo) => $"O campo {nomeCampo} deve ser maior que zero (0)";
    public static string CampoDiferenteZero(string nomeCampo) => $"O campo {nomeCampo} deve ser diferente de zero (0)";
}