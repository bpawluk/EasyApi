using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;

namespace BlazorUtils.EasyApi.Tests.SUT.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddServer(typeof(TestRequest).Assembly);
        var app = builder.Build();
        app.MapRequests(typeof(TestRequest).Assembly);
        app.Run();
    }
}
