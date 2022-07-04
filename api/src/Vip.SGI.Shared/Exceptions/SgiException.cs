using System.Globalization;

namespace Vip.SGI.Shared.Exceptions;

public class SgiException : Exception
{
    #region Construtores

    public SgiException() { }

    public SgiException(string message) : base(message) { }

    public SgiException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

    #endregion
}