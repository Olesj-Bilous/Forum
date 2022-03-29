using Newtonsoft.Json;

namespace Web.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value is null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static bool? GetBoolean(this ISession session, string key)
        {
            byte[] data = session.Get(key);
            return data is null ? null : BitConverter.ToBoolean(data, 0);
        }
    }
}
