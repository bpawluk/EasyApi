using System.Net.Http;

namespace BlazorUtils.EasyApi.Client.Setup;

public interface IHttpClientProvider
{
    HttpClient GetClient(IRequest request);
}
