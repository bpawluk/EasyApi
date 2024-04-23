using BlazorUtils.EasyApi.Benchmarks.SUT.Contract;
using BlazorUtils.EasyApi.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace BlazorUtils.EasyApi.Benchmarks.SUT.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Services.AddEasyApi()
                        .WithContract(typeof(EmptyRequest).Assembly)
                        .WithServer();
        var app = builder.Build();
        app.MapRequests();
        app.Run();
    }
}
