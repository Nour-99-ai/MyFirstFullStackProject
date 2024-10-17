using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class SessionExtensions
{
    // Method to set an object as JSON in session
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    // Method to get an object from JSON in session
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}