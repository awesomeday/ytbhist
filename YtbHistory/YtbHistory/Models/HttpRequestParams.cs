using System.Collections.Generic;
using System.Net.Http;

namespace YtbHistory.Models
{
    class HttpRequestParams
    {
        public string Url { get; set; }

        public HttpMethod Method { get; set; }

        public byte[] Data { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }

        public string Cookies { get; set; }
    }
}
