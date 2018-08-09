using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YtbHistory
{
    class Program
    {
        static string token = "4qmFsgIZEglGRWhpc3RvcnkaDENOT3QzNFA4ME5zQw%253D%253D";
        static string continuation = "4qmFsgIZEglGRWhpc3RvcnkaDENMSzV5dWFDeTlzQw%253D%253D";

        static string urlTemplate = "http://www.youtube.com/browse_ajax?ctoken={0}&continuation={1}&itct=CCEQybcCIhMI0c6p_Ovf3AIVDV-yCh3J5gtV";

        static void Main(string[] args)
        {
            var cookes = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookes, AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            var client = new HttpClient(handler);

            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("VISITOR_INFO1_LIVE", "goenBEt0pBo"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("YSC", "wp61HMG6sNw"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("LOGIN_INFO", "AFmmF2swRQIgblybu7jcsVFb1cmltnVGbBZKqek9RC6m-YQzUjPRRjICIQCxI1SOFnCNiI0fAXgg8tZKpIqowAuJ3qb08XVggL48mA:QUQ3MjNmemZSR1dfckJjYUJpb1ZXYW5ISGZzYmk0Nm9RNWh1NFlYVXFWakZQLWotNFVlM1R6Y2RicWhzN0Q0MmxhbF9jcjAweHYwS1R4Z2c3SzBiS2haRnNSQTJmVERmM3puVFJXc0xhNXlwUlVEMWZRemZWZ2VuZHM4MThHS3d3enhOTk1YWkJCRDNOYjFEY0daQUtXNENFQVJuQ211OUN3a2FpUHZLeWFtWkpYeTZDQzFlRnpz"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("SID", "SgafzzersNeRoNRx9UPJq0CzMHnUxGc8Vbc4TlQNtROhwMNgXSEvWK5w-Ei40UiCt8kDeQ."));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("HSID", "A8WW1Piuohf9HspPQ"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("SSID", "AqPoNqEHmq8dlhecH"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("APISID", "1b_YL6izyEMAtzb1/Ac-kaudREZ78vMJYt"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("SAPISID", "AcVMIA2uQjqLsooS/ACYVq3z3MrwGn2kEn"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("wide", "0"));
            cookes.Add(new Uri("http://www.youtube.com"), new Cookie("PREF", "al=en+ru&f5=30000&f1=50000000"));

            var total = 0;

            while(true)
            {
                var res = client.SendAsync(GetMessage(continuation)).Result;

                var ress = res.Content.ReadAsStringAsync().Result;

                var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RespRoot>>(ress);

                var response = r[1];

                total += response.response.continuationContents.itemSectionContinuation.contents.Count;

                continuation = response.response.continuationContents.itemSectionContinuation.continuations[0].nextContinuationData.continuation;
            }
        }

        public static HttpRequestMessage GetMessage(string continuation)
        {
            var m = new HttpRequestMessage(HttpMethod.Post, new Uri(String.Format(urlTemplate, token, continuation)));

            var cnt = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {  "session_token", "QUFFLUhqbVd3YzQzVDFVUUdpUlFud1psTHdBMGtMVVpvZ3xBQ3Jtc0ttUkdKSmlsQjFRSDVTbjdrZTdZVG9VZWhKQmNRdFRTbExIamp3NksxYTkwRGtRQ1NlUlEyZEVZbmlTLXYwMWdCTmdpdXZZUUUyamcxWHotSHRoY0tKNW5aTmZzUDZhbVhTX2o2WHZmYVRZcnhXZjdpcE1ORGNVUXlqSUN1bXZGQmFqakg1UEswc1BFX0Y3emRaOVFDMHJ5MzVDanc%3D" }
            });

            cnt.Headers.Add("Origin", "http://www.youtube.com");
            cnt.Headers.Add("X-YouTube-Page-Label", "youtube.ytfe.desktop_20180806_5_RC2");
            cnt.Headers.Add("X-YouTube-Variants-Checksum", "3dabe9512fabab7f223b204f575d5316");
            cnt.Headers.Add("X-YouTube-Page-CL", "207789869");
            cnt.Headers.Add("X-SPF-Referer", "http://www.youtube.com/feed/history");
            cnt.Headers.Add("X-YouTube-Utc-Offset", "180");
            cnt.Headers.Add("X-YouTube-Client-Name", "1");
            cnt.Headers.Add("X-SPF-Previous", "http://www.youtube.com/feed/history");
            cnt.Headers.Add("X-YouTube-Client-Version", "2.20180807");
            cnt.Headers.Add("X-Youtube-Identity-Token", "QUFFLUhqbkhNVkc0NjNOaUJGNm0wOTN4N0o0Rkx6MGZ2d3w=");
            cnt.Headers.Add("DNT", "1");
            cnt.Headers.Add("X-Client-Data", "CKa1yQEIhrbJAQiitskBCMG2yQEIqZ3KAQjYncoBCKijygE=");

            m.Content = cnt;
            m.Headers.Add("Referer", "http://www.youtube.com/feed/history");
            m.Headers.Add("Accept", "*/*");
            m.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
            m.Headers.Add("Connection", "keep-alive");
            // m.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            m.Headers.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8");
            m.Headers.Add("Host", "www.youtube.com");
            // m.Headers.Add("Cookie", "VISITOR_INFO1_LIVE=goenBEt0pBo; YSC=wp61HMG6sNw; LOGIN_INFO=AFmmF2swRQIgblybu7jcsVFb1cmltnVGbBZKqek9RC6m-YQzUjPRRjICIQCxI1SOFnCNiI0fAXgg8tZKpIqowAuJ3qb08XVggL48mA:QUQ3MjNmemZSR1dfckJjYUJpb1ZXYW5ISGZzYmk0Nm9RNWh1NFlYVXFWakZQLWotNFVlM1R6Y2RicWhzN0Q0MmxhbF9jcjAweHYwS1R4Z2c3SzBiS2haRnNSQTJmVERmM3puVFJXc0xhNXlwUlVEMWZRemZWZ2VuZHM4MThHS3d3enhOTk1YWkJCRDNOYjFEY0daQUtXNENFQVJuQ211OUN3a2FpUHZLeWFtWkpYeTZDQzFlRnpz; SID=SgafzzersNeRoNRx9UPJq0CzMHnUxGc8Vbc4TlQNtROhwMNgXSEvWK5w-Ei40UiCt8kDeQ.; HSID=A8WW1Piuohf9HspPQ; SSID=AqPoNqEHmq8dlhecH; APISID=1b_YL6izyEMAtzb1/Ac-kaudREZ78vMJYt; SAPISID=AcVMIA2uQjqLsooS/ACYVq3z3MrwGn2kEn; wide=0; PREF=al=en+ru&f5=30000&f1=50000000");

            return m;
        }
    }
}
