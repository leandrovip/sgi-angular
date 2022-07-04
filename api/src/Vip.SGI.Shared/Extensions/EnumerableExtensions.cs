namespace Vip.SGI.Shared.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<string> ToEvents<T>(this IEnumerable<T> notifications, bool addError = false)
    {
        var enumerable = notifications as T[] ?? notifications.ToArray();
        if (!enumerable.Any())
            return new List<string>();

        var type = typeof(T);
        var prop = type.GetProperty("Property");
        var propMessage = type.GetProperty("Message");
        var events = new List<string>();

        if (prop.IsNull()) return events;

        foreach (var item in enumerable)
        {
            var propValue = prop.GetValue(item, null)?.ToString()?.ToLower();

            if (!propValue.Contains("erro") && !propValue.Contains("stacktrace"))
                events.Add(propMessage?.GetValue(item, null).ToString() ?? string.Empty);

            if (addError && propValue.Contains("erro"))
                events.Add(propMessage?.GetValue(item, null).ToString() ?? string.Empty);
        }

        return events;
    }

    public static IEnumerable<string> ToEvents(this Exception exception)
    {
        var events = new List<string> {$"Mensagem: {exception.Message}"};
        if (exception.StackTrace.IsNotNullOrEmpty()) events.Add($"StackTrace: {exception.StackTrace}");
        return events;
    }
}