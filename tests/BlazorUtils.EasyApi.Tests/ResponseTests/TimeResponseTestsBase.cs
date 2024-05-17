using BlazorUtils.EasyApi.Tests.SUT.Contract.Response;
using System.Net;

namespace BlazorUtils.EasyApi.Tests.ResponseTests;

public abstract class TimeResponseTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    public static TheoryData<DateOnly> DateOnlyValues => new()
    {
        DateOnly.MinValue,
        DateOnly.FromDateTime(DateTime.Now),
        DateOnly.MaxValue
    };

    public static TheoryData<DateTimeOffset> DateTimeOffsetValues => new()
    {
        DateTimeOffset.MinValue,
        DateTimeOffset.Now,
        DateTimeOffset.MaxValue
    };

    public static TheoryData<DateTime> DateTimeValues => new()
    {
        DateTime.MinValue,
        DateTime.Now,
        DateTime.MaxValue
    };

    public static TheoryData<TimeOnly> TimeOnlyValues => new()
    {
        TimeOnly.MinValue,
        TimeOnly.FromDateTime(DateTime.Now),
        TimeOnly.MaxValue
    };

    public static TheoryData<TimeSpan> TimeSpanValues => new()
    {
        TimeSpan.MinValue,
        TimeSpan.FromHours(1),
        TimeSpan.MaxValue
    };

    [Theory]
    [MemberData(nameof(DateOnlyValues))]
    public async Task Response_OfDateOnlyType(DateOnly expectedResponse)
    {
        var request = new DateOnlyResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<DateOnlyResponseRequest, DateOnly>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(DateOnlyValues))]
    public async Task Response_OfNullableDateOnlyType_WithValue(DateOnly expectedResponse)
    {
        var request = new NullableDateOnlyResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableDateOnlyResponseRequest, DateOnly?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableDateOnlyType_NoValue()
    {
        var request = new NullableDateOnlyResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableDateOnlyResponseRequest, DateOnly?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetValues))]
    public async Task Response_OfDateTimeOffsetType(DateTimeOffset expectedResponse)
    {
        var request = new DateTimeOffsetResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<DateTimeOffsetResponseRequest, DateTimeOffset>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetValues))]
    public async Task Response_OfNullableDateTimeOffsetType_WithValue(DateTimeOffset expectedResponse)
    {
        var request = new NullableDateTimeOffsetResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableDateTimeOffsetResponseRequest, DateTimeOffset?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableDateTimeOffsetType_NoValue()
    {
        var request = new NullableDateTimeOffsetResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableDateTimeOffsetResponseRequest, DateTimeOffset?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(DateTimeValues))]
    public async Task Response_OfDateTimeType(DateTime expectedResponse)
    {
        var request = new DateTimeResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<DateTimeResponseRequest, DateTime>(request);
        Assert.Equal(request.ExpectedResponse.ToUniversalTime(), result.ToUniversalTime());
    }

    [Theory]
    [MemberData(nameof(DateTimeValues))]
    public async Task Response_OfNullableDateTimeType_WithValue(DateTime expectedResponse)
    {
        var request = new NullableDateTimeResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableDateTimeResponseRequest, DateTime?>(request);
        Assert.Equal(request.ExpectedResponse.Value.ToUniversalTime(), result.Value.ToUniversalTime());
    }

    [Fact]
    public async Task Response_OfNullableDateTimeType_NoValue()
    {
        var request = new NullableDateTimeResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableDateTimeResponseRequest, DateTime?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(TimeOnlyValues))]
    public async Task Response_OfTimeOnlyType(TimeOnly expectedResponse)
    {
        var request = new TimeOnlyResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<TimeOnlyResponseRequest, TimeOnly>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(TimeOnlyValues))]
    public async Task Response_OfNullableTimeOnlyType_WithValue(TimeOnly expectedResponse)
    {
        var request = new NullableTimeOnlyResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableTimeOnlyResponseRequest, TimeOnly?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableTimeOnlyType_NoValue()
    {
        var request = new NullableTimeOnlyResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableTimeOnlyResponseRequest, TimeOnly?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }

    [Theory]
    [MemberData(nameof(TimeSpanValues))]
    public async Task Response_OfTimeSpanType(TimeSpan expectedResponse)
    {
        var request = new TimeSpanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<TimeSpanResponseRequest, TimeSpan>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Theory]
    [MemberData(nameof(TimeSpanValues))]
    public async Task Response_OfNullableTimeSpanType_WithValue(TimeSpan expectedResponse)
    {
        var request = new NullableTimeSpanResponseRequest() { ExpectedResponse = expectedResponse };
        var result = await CallHttp<NullableTimeSpanResponseRequest, TimeSpan?>(request);
        Assert.Equal(request.ExpectedResponse, result);
    }

    [Fact]
    public async Task Response_OfNullableTimeSpanType_NoValue()
    {
        var request = new NullableTimeSpanResponseRequest() { ExpectedResponse = null };
        var caller = GetCaller<NullableTimeSpanResponseRequest, TimeSpan?>();

        var response = await caller.CallHttp(request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.HasResponse);
    }
}
