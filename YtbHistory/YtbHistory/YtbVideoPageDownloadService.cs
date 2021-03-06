﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YtbHistory.Models;

namespace YtbHistory
{
    class YtbVideoPageDownloadService
    {
        private string BaseUrl { get; } = "https://youtube.com";
        private readonly string urlTemplate;
        private readonly string firstPageUrl;

        private YtbVideoPageHttpService httpService;
        private YtbVideoFirstPageHttpService firstPageHttpService;

        private AuthParams authParams;

        public YtbVideoPageDownloadService(AuthParams authParams)
        {
            this.authParams = authParams;

            httpService = new YtbVideoPageHttpService();
            firstPageHttpService = new YtbVideoFirstPageHttpService();

            urlTemplate = $"{BaseUrl}/browse_ajax?ctoken={{0}}&continuation={{1}}";
            firstPageUrl = $"{BaseUrl}/feed/history?pbj=1";
        }

        public async Task<YtbVideoPage> GetPage(string continuation = null)
        {
            if (continuation == null)
            {
                return await GetFirstPage();
            }

            return await httpService.RequestAsync(new HttpRequestParams()
            {
                Method = HttpMethod.Post,
                Url = GetUrl(continuation),
                Data = GetData(),
                Cookies = GetCookies(),
                Headers = GetHeaders()
            });
        }

        private async Task<YtbVideoPage> GetFirstPage()
        {
            return await firstPageHttpService.RequestAsync(new HttpRequestParams()
            {
                Method = HttpMethod.Get,
                Url = firstPageUrl,
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
