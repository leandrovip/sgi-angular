using System.ComponentModel;

namespace Vip.SGI.Domain.Enums;

public enum RegimeTributario
{
    [Description("1 - Simples Nacional")] SimplesNacional = 1,
    [Description("2 - Simples Nacional Excesso de Receita Bruta")] SimplesNacionalExcessoReceitaBruta = 2,
    [Description("3 - Regime Normal")] Normal = 3
}