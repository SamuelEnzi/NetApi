using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ApiCore
{
    /// <summary>
    /// contains basic extensions for general uses
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// deserializes string into object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T? Deserialize<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch { return default; }
        }

        /// <summary>
        /// serializs object into json string
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        public static string Serialize(this object Object) =>
            JsonConvert.SerializeObject(Object);

        /// <summary>
        /// trims all space, & and ? charactes from both ends of a string
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string TrimUrl(this string Url) =>
            Url.Trim().Trim('&').Trim('?');


        public static void ForEach<T>(this IEnumerable<T> list, Action<T> Do) =>
            list.ToList().ForEach((x) => Do(x));
    }
}
