using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YtbHistory.Models;

namespace YtbHistory
{
    class YtbVideoPageHttpService : HttpService<YtbVideoPage>
    {
        protected async override Task<YtbVideoPage> ProcessResultAsync(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RespRoot>>(json);

            var result = new YtbVideoPage();

            var pageData = data[1];

            result.Videos = pageData.response.continuationContents.itemSectionContinuation.contents.Select((video) => {
                var description = video.videoRenderer?.descriptionSnippet?.simpleText;
                var id = video.videoRenderer.videoId;
                var thumb = video.videoRenderer?.thumbnail?.thumbnails[0]?.url;
                var title = video.videoRenderer?.title.simpleText;
                var channelId = video.videoRenderer?.ownerText?.runs[0]?.navigationEndpoint?.browseEndpoint?.browseId;
                var channelName = video.videoRenderer?.ownerText?.runs[0]?.text;

                return new YtbVideo()
                {
                    Id = id,
                    Description = description,
                    Thumbnail = thumb,
                    Title = title,
                    ChannelId = channelId,
                    ChannelName = channelName
                };
            }).ToArray();

            result.ContinuationToken = pageData.response.continuationContents.itemSectionContinuation.continuations[0].nextContinuationData.continuation;

            return result;
        }
    }
}
