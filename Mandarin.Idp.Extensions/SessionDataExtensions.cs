using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mandarin.Idp.Extensions;

public static class SessionDataExtensions
{
    public static T? Get<T>(this ISession session, string key) where T: class
    {
        var userInfo = session.GetString(key);
        return userInfo == null ? null : JsonConvert.DeserializeObject<T>(userInfo);
    }

    public static void Set<T>(this ISession session, string key, T data) where T: class
    {
        if (data == null) throw new ArgumentNullException();
                
        session.SetString(key, JsonConvert.SerializeObject(data));
    }
}