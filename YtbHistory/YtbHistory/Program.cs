using System;
using System.Linq;
using YtbHistory.Models;

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
            var authParams = new AuthParams
            {
                Cookie = "VISITOR_INFO1_LIVE=WKRX3izQ8RI; YSC=6sVMmvB6EfI; LOGIN_INFO=AFmmF2swRgIhAMKT7RF2fCP2cWPwbk2FrH42Gqfq0iAxcdP7eRkgbD1fAiEAx8D-w06SKo8Fkg5Z45r849QsPn5LlkDq-d_pbJTLE48:QUQ3MjNmeVdGbkdkcHpHMHRzcGQyTTFRMlJ2Rm5lZzJHUllOMUpyTlJYYWcyZjJ4Y3UwU1o2RUk1cC10SWRYMHRYNFNCeE9QdXEtUjR6UnNpMUVvQVUtZ2xSNXJkcDZyNWlDcjNpR1d4WTlpM2FEZWxoU1ZmLWk3T2Y4ZkZkVzlJUTRMUlBITUp0bzh1bGNEWHFYb05KMl9BVzNtZ3RrV1Nnb0FUbTlDQXZVZjc5a0pKR3JVT0FV; SID=Pwafz7p6hFkKvNYQgpSucd39yWfK3eCtGUNMLszhYKIBbTrDFyUmVXo5Iqw6hLYw7rV1vA.; HSID=AKPbdY38rPNybw0_w; SSID=AmjCuxgeWr0MOd30D; APISID=MhrptDn8XqWzn9Pk/Ao5fY7xkvFzH5F2qa; SAPISID=w0WiLi2c4lzws6PX/As9Xumm0iTI2gw5en; wide=0; PREF=f1=50000000&al=en+ru&f5=30000&f6=1",
                CToken = "4qmFsgIZEglGRWhpc3RvcnkaDENPMnc1cHZRdXR3Qw%253D%253D",
                SessionToken = "QUFFLUhqbVd3YzQzVDFVUUdpUlFud1psTHdBMGtMVVpvZ3xBQ3Jtc0ttUkdKSmlsQjFRSDVTbjdrZTdZVG9VZWhKQmNRdFRTbExIamp3NksxYTkwRGtRQ1NlUlEyZEVZbmlTLXYwMWdCTmdpdXZZUUUyamcxWHotSHRoY0tKNW5aTmZzUDZhbVhTX2o2WHZmYVRZcnhXZjdpcE1ORGNVUXlqSUN1bXZGQmFqakg1UEswc1BFX0Y3emRaOVFDMHJ5MzVDanc%3D",
                XClientData = "CI+2yQEIo7bJAQjEtskBCKmdygEI2J3KAQjancoBCKijygEIgqTKAQiUp8oB",
                XYoutubeIdentityToken = "QUFFLUhqbkhNVkc0NjNOaUJGNm0wOTN4N0o0Rkx6MGZ2d3w=",
                XYoutubePageCl = "207856115",
                XYoutubePageLabel = "youtube.ytfe.desktop_20180807_8_RC1",
                XYoutubeVariantsChecksum = "aea0182d8fdec10710d63a72f6917a1f"
            };

            var downloader = new YtbVideoPageDownloadService(authParams);

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
