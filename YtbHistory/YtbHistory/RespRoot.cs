using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YtbHistory
{
    class SimpleText
    {
        public string simpleText { get; set; }
    }

    class ThumbnailInfo
    {
        public string url { get; set; }
    }

    class Thumbnail
    {
        public List<ThumbnailInfo> thumbnails { get; set; }
    }

    class WatchEndpoint
    {
        public string videoId { get; set; }
    }

    class NavigationEndpoint
    {
        public WatchEndpoint watchEndpoint { get; set; }
    }

    class VideoRenderer
    {
        public SimpleText descriptionSnippet { get; set; }

        public Thumbnail thumbnail { get; set; }

        public NavigationEndpoint navigationEndpoint { get; set; }
    }

    class VideoData
    {
        public VideoRenderer videoRenderer { get; set; }
    }

    class NextContinuationData
    {
        public string continuation { get; set; }
    }

    class Continuation
    {
        public NextContinuationData nextContinuationData { get; set; }
    }

    class ItemSectionContinuation
    {
        public List<VideoData> contents { get; set; }

        public List<Continuation> continuations { get; set; }
    }

    class ContinuationContents
    {
        public ItemSectionContinuation itemSectionContinuation { get; set; }
    }

    class YtbResponse
    {
        public ContinuationContents continuationContents { get; set; }
    }

    class RespRoot
    {
        public YtbResponse response { get; set; }
    }
}
