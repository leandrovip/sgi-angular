using System.ComponentModel;

namespace Vip.SGI.Domain.Enums;

public enum UsuarioFuncao
{
    [Description("Colaborador")] Colaborador = 0,
    [Description("Administrador")] Administrador = 1,
}