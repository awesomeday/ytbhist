using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YtbHistory
{
    class VideoPageDownloadService
    {
        private string BaseUrl { get; } = "https://youtube.com";
        private readonly string urlTemplate;

        private YtbVideoPageHttpService httpService;
        private AuthParams authParams;

        public VideoPageDownloadService()
        {
            authParams = new AuthParams();
            httpService = new YtbVideoPageHttpService();
            urlTemplate = $"{BaseUrl}/browse_ajax?ctoken={{0}}&continuation={{1}}"; // &itct=CCEQybcCIhMI0c6p_Ovf3AIVDV-yCh3J5gtV
        }

        public async Task<YtbVideoPage> GetPage(string continuation)
        {
            return await httpService.RequestAsync(new HttpRequestParams()
            {
                Method = HttpMethod.Post,
                Url = GetUrl(continuation),
                Data = GetData(),
                Cookies = GetCookies(),
                Headers = GetHeaders()
            });
        }

        private string GetUrl(string continuation)
        {
            return String.Format(urlTemplate, authParams.CToken, continuation);
        }

        private byte[] GetData()
        {
            return Encoding.ASCII.GetBytes($"session_token={authParams.SessionToken}");
        }

        private string GetCookies()
        {
            return authParams.Cookie;
        }

        private Dictionary<string, string> GetHeaders()
        {
            return new Dictionary<string, string>
            {
                { "Origin", BaseUrl },
                { "X-YouTube-Page-Label", authParams.XYoutubePageLabel },
                { "X-YouTube-Variants-Checksum", authParams.XYoutubeVariantsChecksum },
                { "X-YouTube-Page-CL", authParams.XYoutubePageCl },
                { "X-SPF-Referer", $"{BaseUrl}/feed/history" },
                { "X-YouTube-Utc-Offset", "180" },
                { "X-YouTube-Client-Name", "1" },
                { "X-SPF-Previous", $"{BaseUrl}/feed/history" },
                { "X-YouTube-Client-Version", "2.20180807" },
                { "X-Youtube-Identity-Token", authParams.XYoutubeIdentityToken },
                { "X-Client-Data", authParams.XClientData },
                { "DNT", "1" },

                { "Referer", $"{BaseUrl}/feed/history" },
                { "Accept", "*/*" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" },
                { "Connection", "keep-alive" },
                { "Accept-Language", "en-US,en;q=0.9,ru;q=0.8" },
                { "Host", "www.youtube.com" }
            };
        }
    }
}
