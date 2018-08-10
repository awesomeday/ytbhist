using System.Threading.Tasks;
using YtbHistory.Models;

namespace YtbHistory
{
    interface IHttpService<TResult>
    {
        Task<TResult> RequestAsync(HttpRequestParams parameters);
    }
}
