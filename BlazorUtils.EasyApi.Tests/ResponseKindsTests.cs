using BlazorUtils.EasyApi.Tests.SUT.Contract;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using static BlazorUtils.EasyApi.Tests.SUT.Contract.WithResponseRequest;

namespace BlazorUtils.EasyApi.Tests;

public class ResponseKindsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    public static TheoryData<HttpStatusCode> SuccessStatusCodes => new()
    {
        HttpStatusCode.OK,
        HttpStatusCode.Created,
        HttpStatusCode.Accepted,
        HttpStatusCode.NoContent
    };

    public static TheoryData<HttpStatusCode> FailureStatusCodes => new()
    {
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden,
        HttpStatusCode.NotFound,
        HttpStatusCode.UnprocessableEntity,
        HttpStatusCode.InternalServerError,
        HttpStatusCode.NotImplemented
    };

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForNoResponse_WithSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        var caller = _sut.Services.GetRequiredService<ICall<NoResponseRequest>>();
        await caller.Call(request, CancellationToken.None);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task Request_ForNoResponse_WithFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        var caller = _sut.Services.GetRequiredService<ICall<NoResponseRequest>>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForHttpResult_WithSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        await CallHttp(request, expectedStatusCode);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task Request_ForHttpResult_WithFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        await CallHttp(request, expectedStatusCode);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForResponse_WithSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = _sut.Services.GetRequiredService<ICall<WithResponseRequest, Response>>();

        var response = await caller.Call(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(request.Id, response.Id);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForResponse_WithSuccessStatus_ButMissingResponse(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = false
        };
        var caller = _sut.Services.GetRequiredService<ICall<WithResponseRequest, Response>>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
        Assert.Contains("missing Response", exception.Message);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task Request_ForResponse_WithFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = _sut.Services.GetRequiredService<ICall<WithResponseRequest, Response>>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
        Assert.DoesNotContain("missing Response", exception.Message);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForHttpResult_WithResponse_AndSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var response = await CallHttp<WithResponseRequest, Response>(request, expectedStatusCode);
        Assert.Equal(request.Id, response.Id);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Request_ForHttpResult_WithResponse_AndSuccessStatus_ButMissingTheResponse(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = false
        };
        var caller = _sut.Services.GetRequiredService<ICall<WithResponseRequest, Response>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.False(response.HasResponse);
        Assert.Null(response.Response);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task Request_ForHttpResult_WithResponse_AndFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = _sut.Services.GetRequiredService<ICall<WithResponseRequest, Response>>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(response.HasResponse);
        Assert.NotNull(response.Response);
        Assert.Equal(request.Id, response.Response.Id);
    }
}
