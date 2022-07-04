using System.ComponentModel;

namespace Vip.SGI.Domain.Enums;

public enum NaturezaJuridica
{
    [Description("0 - Empresário Individual (EI)")]
    EmpresarioIndividual = 0,

    [Description("1 - Microempreendedor Individual (MEI)")]
    MicroempreendedorIndividual = 1,

    [Description("2 - Empresa Individual de Resp. Limitada (EIRELI)")]
    EmpresaIndividual = 2,

    [Description("3 - Sociedade Simples")] SociedadeSimples = 3,

    [Description("4 - Sociedade Empresária Limitada (LTDA)")]
    SociedadeEmpresariaLimitada = 4,

    [Description("5 - Sociedade Anônima (SA)")]
    SociedadeAnonima = 5,
}