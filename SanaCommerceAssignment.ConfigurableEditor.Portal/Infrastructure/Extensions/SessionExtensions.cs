using Newtonsoft.Json;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Extensions;
public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        var serializedContent = JsonConvert.SerializeObject(value);
        session.SetString(key, serializedContent);
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value)!;
    }
}