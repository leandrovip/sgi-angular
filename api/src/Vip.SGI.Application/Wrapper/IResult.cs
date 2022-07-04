namespace Vip.SGI.Application.Wrapper;

public interface IResult
{
    bool Success { get; }
    string Message { get; }
    IEnumerable<string> Events { get; }
}