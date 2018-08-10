using System.Collections.Generic;

namespace YtbHistory.Models
{
    class YtbVideoPage
    {
        public IEnumerable<YtbVideo> Videos { get; set; }

        public string ContinuationToken { get; set; }
    }
}
