using System.Threading.Tasks;

namespace YtbHistory
{
    interface IHttpService<TResult>
    {
        Task<TResult> RequestAsync(HttpRequestParams parameters);
    }
}
