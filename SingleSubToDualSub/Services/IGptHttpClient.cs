using SingleSubToDualSub.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SingleSubToDualSub.Services
{
    public interface IGptHttpClient
    {
        Task<HttpResponseMessage> GetCompletion(RequestData requestData, string apiKey);
        Task<HttpResponseMessage> GetCompletionStream(RequestData requestData, string apiKey);
        // IAsyncEnumerable<ChatCompletionCreateResponse> CreateCompletionAsStream(RequestData requestData, string apiKey, CancellationToken cancellationToken = default);

    }
}
