using System.Collections.Generic;

namespace YtbHistory
{
    class YtbVideoPage
    {
        public IEnumerable<YtbVideo> Videos { get; set; }

        public string ContinuationToken { get; set; }
    }
}
