using System.Text.Json;
using Vip.SGI.Shared.Constants;

namespace Vip.SGI.Shared.Extensions;

public static class StringExtensions
{
    public static string Encrypt(this string value)
    {
        return value.Encrypt(Settings.CryptoKey);
    }

    public static string Decrypt(this string value)
    {
        return value.Decrypt(Settings.CryptoKey);
    }

    public static string ToJson<T>(this T value) where T : class
    {
        return JsonSerializer.Serialize(value);
    }
}