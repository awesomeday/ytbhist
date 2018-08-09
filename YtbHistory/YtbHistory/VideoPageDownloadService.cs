using System.Threading.Tasks;

namespace YtbHistory
{
    class VideoPageDownloadService
    {
        private YtbVideoPageHttpService httpService;

        public VideoPageDownloadService()
        {
            httpService = new YtbVideoPageHttpService();
        }

        public async Task<YtbVideoPage> GetPage(string continuation)
        {
            return await httpService.RequestAsync(new HttpRequestParams());
        }

        private string GetUrl()
        {

        }
    }
}
