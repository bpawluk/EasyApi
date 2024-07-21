using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BlazorUtils.EasyApi.Client.Setup;

internal class DefaultHttpClientProvider(IServiceProvider serviceProvider) : IHttpClientProvider
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public HttpClient GetClient(IRequest _) => _serviceProvider.GetRequiredService<HttpClient>();
}
