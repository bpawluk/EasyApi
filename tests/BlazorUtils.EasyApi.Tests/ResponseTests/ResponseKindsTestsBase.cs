using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests;

public abstract class ResponseKindsTestsBase(TestsFixture fixture) : TestsBase(fixture)
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
    public async Task NoResponse_WithSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        var caller = GetCaller<NoResponseRequest>();

        await caller.Call(request, CancellationToken.None);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task NoResponse_WithFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new NoResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode
        };
        var caller = GetCaller<NoResponseRequest>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task HttpResult_WithSuccessStatus(HttpStatusCode expectedStatusCode)
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
    public async Task HttpResult_WithFailedStatus(HttpStatusCode expectedStatusCode)
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
    public async Task Response_WithSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = GetCaller<WithResponseRequest, WithResponseRequest.Response>();

        var response = await caller.Call(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(request.Id, response.Id);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task Response_WithSuccessStatus_ButMissingResponse(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = false
        };
        var caller = GetCaller<WithResponseRequest, WithResponseRequest.Response>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
        Assert.Contains("missing Response", exception.Message);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task Response_WithFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = GetCaller<WithResponseRequest, WithResponseRequest.Response>();

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => caller.Call(request, CancellationToken.None));

        Assert.Contains(((int)expectedStatusCode).ToString(), exception.Message);
        Assert.DoesNotContain("missing Response", exception.Message);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task HttpResult_WithResponse_AndSuccessStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };

        var response = await CallHttp<WithResponseRequest, WithResponseRequest.Response>(request, expectedStatusCode);

        Assert.Equal(request.Id, response.Id);
    }

    [Theory]
    [MemberData(nameof(SuccessStatusCodes))]
    public async Task HttpResult_WithResponse_AndSuccessStatus_ButMissingTheResponse(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = false
        };
        var caller = GetCaller<WithResponseRequest, WithResponseRequest.Response>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.False(response.HasResponse);
        Assert.Null(response.Response);
    }

    [Theory]
    [MemberData(nameof(FailureStatusCodes))]
    public async Task HttpResult_WithResponse_AndFailedStatus(HttpStatusCode expectedStatusCode)
    {
        var request = new WithResponseRequest()
        {
            Id = Guid.NewGuid(),
            ExpectedStatusCode = expectedStatusCode,
            IncludeResponse = true
        };
        var caller = GetCaller<WithResponseRequest, WithResponseRequest.Response>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.True(response.HasResponse);
        Assert.NotNull(response.Response);
        Assert.Equal(request.Id, response.Response.Id);
    }
}
