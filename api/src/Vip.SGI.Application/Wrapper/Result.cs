namespace Vip.SGI.Application.Wrapper;

public class Result : IResult
{
    #region Propriedades

    public bool Success { get; private set; }
    public string Message { get; private set; }
    public IEnumerable<string> Events { get; private set; }
    public virtual object Data { get; private set; }

    #endregion

    #region Construtores

    public Result(bool success, string message, IEnumerable<string> events, object data)
    {
        Success = success;
        Message = message;
        Events = events ?? new List<string>();
        Data = data;
    }

    #endregion

    #region Métodos Estáticos

    public static Result Factory(bool success, string message, IEnumerable<string> events = null, object data = null)
    {
        return new(success, message, events, data);
    }

    #endregion
}

public class Result<T> : Result
{
    #region Propriedades

    public new T Data { get; private set; }

    #endregion

    #region Construtores

    public Result(bool success, string message, T data, IEnumerable<string> events) : base(success, message, events, data)
    {
        Data = data;
    }

    #endregion
}