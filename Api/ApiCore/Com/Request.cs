using System.Web;

namespace ApiCore.Com
{
    /// <summary>
    /// defines basic <see cref="Request"/> behavior; creats a <see cref="HttpClient"/> containing all basic information;
    /// call <see cref="Submit(string)"/> to create a <see cref="HttpRequestMessage"/> and send it
    /// </summary>
    public class Request
    {
        public readonly HttpClient client = new HttpClient();

        private string getRequestString = "";
        private List<KeyValuePair<string, string>> postRequestContent = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// appends a get parameter
        /// </summary>
        /// <param name="pair"></param>
        public void AppendGet((string key, string? value) pair)
        {
            if (pair.value == null) return;
            getRequestString = (getRequestString + $"&{pair.key.TrimUrl()}={HttpUtility.UrlEncode(pair.value!.TrimUrl())}").TrimUrl();
        }

        /// <summary>
        /// appends a post parameter
        /// </summary>
        /// <param name="pair"></param>
        public void AppendPost((string key, string? value) pair)
        {
            if (pair.value == null) return;
            postRequestContent.Add(new KeyValuePair<string, string>(pair.key,pair.value));
        }

        /// <summary>
        /// appends a header element
        /// </summary>
        /// <param name="pair"></param>
        public void AppendHeader((string key, string? value) pair)
        {
            if (pair.value == null) return;
            client.DefaultRequestHeaders.Add(pair.key,pair.value);
        }

        /// <summary>
        /// submits the request using containing request info
        /// </summary>
        /// <param name="url">should contain the server url with protocol</param>
        /// <returns>returns async <see cref="HttpResponseMessage"/></returns>
        public virtual async Task<HttpResponseMessage> Submit(string url) =>
            await Send(url);

        /// <summary>
        /// <inheritdoc cref="Request.Submit(string)"/>
        /// parses the request into new object of type T assuming the server response is json formatted
        /// </summary>
        /// <typeparam name="T">result type</typeparam>
        /// <param name="url">should contain the server url with protocol</param>
        /// <returns>returns a instance of a new object as result and the <see cref="HttpResponseMessage"/></returns>
        public virtual async Task<(T? result, HttpResponseMessage response)> Submit<T>(string url)
        {
            var response = await Send(url);
            var result = (await response.Content.ReadAsStringAsync()).Deserialize<T>();
            return (result, response);
        }

        private async Task<HttpResponseMessage> Send(string url)
        {
            try
            {
                Com.Progress.Register(this.client, this);
                var requestPath = $"{url.TrimUrl()}?{getRequestString.TrimUrl()}";
                var body = new FormUrlEncodedContent(postRequestContent);
                return await client.PostAsync(requestPath, body);
            }
            finally
            {
                Com.Progress.Unregiser(this.client, this);
            }
        }
    }
}
