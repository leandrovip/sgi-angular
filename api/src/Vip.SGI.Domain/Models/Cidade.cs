using Vip.SGI.Domain.Models.Base;

namespace Vip.SGI.Domain.Models;

public class Cidade : EntidadeBase
{
    #region Propriedades

    public int CidadeId { get; private set; }
    public int EstadoId { get; private set; }
    public string Nome { get; private set; }
    public string Estado { get; private set; }

    #endregion

    #region Construtores

    public Cidade(int cidadeId, int estadoId, string nome, string estado)
    {
        CidadeId = cidadeId;
        EstadoId = estadoId;
        Nome = nome;
        Estado = estado;
    }

    protected Cidade() { }

    #endregion

    #region Constantes

    public const int NomeMaxLength = 50;
    public const int EstadoMaxLength = 2;

    #endregion
}