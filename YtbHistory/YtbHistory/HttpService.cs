using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace YtbHistory
{
    abstract class HttpService<TResult> : IHttpService<TResult>
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        public HttpService()
        {
            httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<TResult> RequestAsync(HttpRequestParams parameters)
        {    
            httpClientHandler.CookieContainer.SetCookies(new Uri("http://www.youtube.com"), parameters.Cookies.Replace(';', ','));
            var result = await httpClient.SendAsync(GetRequestMessage(parameters));

            return await ProcessResultAsync(result);
        }

        protected abstract Task<TResult> ProcessResultAsync(HttpResponseMessage response);

        private HttpRequestMessage GetRequestMessage(HttpRequestParams parameters)
        {
            var m = new HttpRequestMessage(parameters.Method, new Uri(parameters.Url))
            {
                Content = parameters.Data != null ? new ByteArrayContent(parameters.Data) : null
            };

            foreach(var pair in parameters.Headers)
            {
                m.Headers.Add(pair.Key, pair.Value);
            }

            return m;
        }
    }
}
