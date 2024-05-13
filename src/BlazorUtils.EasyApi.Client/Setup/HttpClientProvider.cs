using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BlazorUtils.EasyApi.Client.Setup;

internal class HttpClientProvider : IHttpClientProvider
{
    private readonly IServiceProvider _serviceProvider;

    public HttpClientProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public HttpClient GetClient(IRequest _) => _serviceProvider.GetRequiredService<HttpClient>();
}
