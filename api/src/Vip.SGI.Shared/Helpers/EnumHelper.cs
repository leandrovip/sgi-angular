namespace Vip.SGI.Shared.Helpers;

public static class EnumHelper
{
    public static IEnumerable<string> GetDescriptions<T>() where T : struct, IConvertible
    {
        var type = typeof(T);
        if (!type.IsEnum || typeof(T) != type)
            return new List<string>();

        try
        {
            return Enum.GetValues(type).Cast<T>().Select(item => item.GetDescription()).ToList();
        }
        catch
        {
            return new List<string>();
        }
    }

    public static T GetEnum<T>(string description) where T : struct, IConvertible
    {
        var type = typeof(T);
        var value = Enum.GetValues(type).Cast<T>().FirstOrDefault(x => x.GetDescription() == description);
        return value;
    }
}