using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEasyApi()
                        .WithContract(typeof(TestRequest).Assembly)
                        .WithServer();
        var app = builder.Build();
        app.MapRequests();
        app.Run();
    }
}
