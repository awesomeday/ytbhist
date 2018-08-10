using System;
using System.Collections.Generic;
using System.Linq;
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
            httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<TResult> RequestAsync(HttpRequestParams parameters)
        {    
            httpClientHandler.CookieContainer = ParseCookies(parameters.Cookies);
            var result = await httpClient.SendAsync(GetRequestMessage(parameters));

            return await ProcessResultAsync(result);
        }

        protected abstract Task<TResult> ProcessResultAsync(HttpResponseMessage response);

        private CookieContainer ParseCookies(string cookies)
        {
            var container = new CookieContainer();

            var parts = cookies.Split(';');

            foreach(var part in parts.Select((p) => p.Trim()))
            {
                var firstEq = part.IndexOf('=');

                if (firstEq > 0)
                {
                    var name = part.Substring(0, firstEq);
                    var value = part.Substring(firstEq + 1);

                    container.Add(new Uri("http://www.youtube.com"), new Cookie(name, value));
                }
            }

            return container;
        }

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

            //m.Headers.Add("Origin", "http://www.youtube.com");
            //m.Headers.Add("X-YouTube-Page-Label", "youtube.ytfe.desktop_20180806_5_RC2");
            //m.Headers.Add("X-YouTube-Variants-Checksum", "3dabe9512fabab7f223b204f575d5316");
            //m.Headers.Add("X-YouTube-Page-CL", "207789869");
            //m.Headers.Add("X-SPF-Referer", "http://www.youtube.com/feed/history");
            //m.Headers.Add("X-YouTube-Utc-Offset", "180");
            //m.Headers.Add("X-YouTube-Client-Name", "1");
            //m.Headers.Add("X-SPF-Previous", "http://www.youtube.com/feed/history");
            //m.Headers.Add("X-YouTube-Client-Version", "2.20180807");
            //m.Headers.Add("X-Youtube-Identity-Token", "QUFFLUhqbkhNVkc0NjNOaUJGNm0wOTN4N0o0Rkx6MGZ2d3w=");
            //m.Headers.Add("DNT", "1");
            //m.Headers.Add("X-Client-Data", "CKa1yQEIhrbJAQiitskBCMG2yQEIqZ3KAQjYncoBCKijygE=");

            //m.Headers.Add("Referer", "http://www.youtube.com/feed/history");
            //m.Headers.Add("Accept", "*/*");
            //m.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
            //m.Headers.Add("Connection", "keep-alive");
            //m.Headers.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8");
            //m.Headers.Add("Host", "www.youtube.com");

            // m.Headers.Add("Cookie", "VISITOR_INFO1_LIVE=goenBEt0pBo; YSC=wp61HMG6sNw; LOGIN_INFO=AFmmF2swRQIgblybu7jcsVFb1cmltnVGbBZKqek9RC6m-YQzUjPRRjICIQCxI1SOFnCNiI0fAXgg8tZKpIqowAuJ3qb08XVggL48mA:QUQ3MjNmemZSR1dfckJjYUJpb1ZXYW5ISGZzYmk0Nm9RNWh1NFlYVXFWakZQLWotNFVlM1R6Y2RicWhzN0Q0MmxhbF9jcjAweHYwS1R4Z2c3SzBiS2haRnNSQTJmVERmM3puVFJXc0xhNXlwUlVEMWZRemZWZ2VuZHM4MThHS3d3enhOTk1YWkJCRDNOYjFEY0daQUtXNENFQVJuQ211OUN3a2FpUHZLeWFtWkpYeTZDQzFlRnpz; SID=SgafzzersNeRoNRx9UPJq0CzMHnUxGc8Vbc4TlQNtROhwMNgXSEvWK5w-Ei40UiCt8kDeQ.; HSID=A8WW1Piuohf9HspPQ; SSID=AqPoNqEHmq8dlhecH; APISID=1b_YL6izyEMAtzb1/Ac-kaudREZ78vMJYt; SAPISID=AcVMIA2uQjqLsooS/ACYVq3z3MrwGn2kEn; wide=0; PREF=al=en+ru&f5=30000&f1=50000000");

            return m;
        }
    }
}
