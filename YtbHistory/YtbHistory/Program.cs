using System;

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

            var page = await downloader.GetPage("4qmFsgIZEglGRWhpc3RvcnkaDENPMnc1cHZRdXR3Qw%253D%253D");
        }
    }
}
