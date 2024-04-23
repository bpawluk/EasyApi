using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Params;
using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using BlazorUtils.EasyApi.Tests.SUT.Server.Params;
using BlazorUtils.EasyApi.Tests.SUT.Server.Response;

namespace BlazorUtils.EasyApi.Tests.SUT.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEasyApi()
                        .WithContract(
                            typeof(GetRequest).Assembly,
                            typeof(HeaderParamRequest).Assembly,
                            typeof(NoResponseRequest).Assembly)
                        .WithServer(
                            typeof(ParamKindsRequestsHandler).Assembly, 
                            typeof(ResponseKindsRequestsHandler).Assembly);
        var app = builder.Build();
        app.MapRequests();
        app.Run();
    }
}
