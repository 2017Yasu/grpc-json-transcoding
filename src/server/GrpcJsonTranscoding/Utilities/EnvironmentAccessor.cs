namespace GrpcJsonTranscoding.Utilities;

/// <summary>
/// Environment variable accessor
/// </summary>
public static class EnvironmentAccessor
{
    /// <summary>
    /// Get environment variable as string.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetAsString(string key, string defaultValue)
    {
        var v = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(v) ? defaultValue : v;
    }

    /// <summary>
    /// Get environment variable as int.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetAsInt(string key, int defaultValue)
    {
        var v = Environment.GetEnvironmentVariable(key);
        if (int.TryParse(v, out int result))
        {
            return result;
        }
        return defaultValue;
    }
}
