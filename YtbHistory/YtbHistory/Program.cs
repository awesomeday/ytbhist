using System;
using System.Linq;

namespace YtbHistory
{
    class Program
    {
        static void Main()
        {
            Download();
            Console.ReadKey();
        }

        static async void Download()
        {
            var authParams = new AuthParams();
            var downloader = new VideoPageDownloadService();

            var continuation = (string)null;
            var total = 0;

            while(true)
            {
                var page = await downloader.GetPage(continuation);

                total += page.Videos.Count();
                continuation = page.ContinuationToken;
            }
        }
    }
}
