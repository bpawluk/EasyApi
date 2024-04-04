using System.Net.Http;

namespace BlazorUtils.EasyApi.Client;

public interface IHttpClientProvider
{
    HttpClient GetClient(IRequest request);
}
