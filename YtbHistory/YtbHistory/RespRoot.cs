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

    class BrowseEndpoint
    {
        public string browseId { get; set; }
    }

    class NavigationEndpoint
    {
        public BrowseEndpoint browseEndpoint { get; set; }
    }

    class OwnerTextRun
    {
        public NavigationEndpoint navigationEndpoint { get; set; }

        public string text { get; set; }
    }

    class OwnerText
    {
        public List<OwnerTextRun> runs { get; set; }
    }

    class VideoRenderer
    {
        public SimpleText descriptionSnippet { get; set; }

        public SimpleText title { get; set; }

        public Thumbnail thumbnail { get; set; }

        public string videoId { get; set; }

        public OwnerText ownerText { get; set; }
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









    class ListRendererContent
    {
        public ItemSectionContinuation itemSectionRenderer { get; set; }
    }

    class SectionListRenderer
    {
        public List<ListRendererContent> contents { get; set; }
    }

    class TabContent
    {
        public SectionListRenderer sectionListRenderer { get; set; }
    }

    class TabRenderer
    {
        public TabContent content { get; set; }
    }

    class FirstPageTab
    {
        public TabRenderer tabRenderer { get; set; }
    }

    class TwoColumnBrowseResultsRenderer
    {
        public List<FirstPageTab> tabs { get; set; }
    }

    class FirstPageContents
    {
        public TwoColumnBrowseResultsRenderer twoColumnBrowseResultsRenderer { get; set; }
    }

    class FirstPageYtbResponse
    {
        public FirstPageContents contents { get; set; }
    }

    class FirstPageResponseRoot
    {
        public FirstPageYtbResponse response { get; set; }
    }
}
