namespace Vip.SGI.Application.Wrapper;

public class ResultList<T> : IResult
{
    #region Propriedades

    public bool Success { get; set; }
    public string Message { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public IEnumerable<T> Data { get; set; }
    public IEnumerable<string> Events { get; set; }

    #endregion

    #region Construtores

    public ResultList() { }

    public ResultList(bool success, string message, IEnumerable<T> data, int count, int pageNumber, int pageSize, IEnumerable<string> events)
    {
        Success = success;
        Message = message;
        Data = data;
        Events = events ?? new List<string>();
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        TotalCount = count;
    }

    #endregion

    #region Métodos Estáticos

    public static ResultList<T> Fail(string message, IEnumerable<string> events)
    {
        return new(false, message, new List<T>(), 0, 0, 0, events);
    }

    public static ResultList<T> Ok(string message, IEnumerable<T> data, int count, int pageNumber, int pageSize)
    {
        return new(true, message, data, count, pageNumber, pageSize, null);
    }

    #endregion
}