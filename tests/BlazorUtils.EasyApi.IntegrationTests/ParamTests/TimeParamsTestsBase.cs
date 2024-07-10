using BlazorUtils.EasyApi.IntegrationTests.SUT.Contract.Params;

namespace BlazorUtils.EasyApi.IntegrationTests.ParamTests;

public abstract class TimeParamsTestsBase(TestsFixture fixture) : TestsBase(fixture)
{
    [Fact]
    public async Task Request_WithTimeParams()
    {
        var request = new TimeParamsRequest()
        {
            DateOnly = DateOnly.FromDateTime(DateTime.Now),
            DateOnlyDefault = default,
            NullableDateOnlyWithValue = DateOnly.MaxValue,
            NullableDateOnlyDefault = null,
            DateTimeOffset = DateTimeOffset.Now,
            DateTimeOffsetDefault = default,
            NullableDateTimeOffsetWithValue = DateTimeOffset.MaxValue,
            NullableDateTimeOffsetDefault = null,
            DateTime = DateTime.Now,
            DateTimeDefault = default,
            NullableDateTimeWithValue = DateTime.MaxValue,
            NullableDateTimeDefault = null,
            TimeOnly = TimeOnly.FromDateTime(DateTime.Now),
            TimeOnlyDefault = default,
            NullableTimeOnlyWithValue = TimeOnly.MaxValue,
            NullableTimeOnlyDefault = null,
            TimeSpan = TimeSpan.FromHours(1),
            TimeSpanDefault = default,
            NullableTimeSpanWithValue = TimeSpan.MaxValue,
            NullableTimeSpanDefault = null,
        };

        var result = await CallHttp<TimeParamsRequest, TimeParamsRequest.Response>(request);

        Assert.Equal(request.DateOnly, result.DateOnly);
        Assert.Equal(request.DateOnlyDefault, result.DateOnlyDefault);
        Assert.Equal(request.NullableDateOnlyWithValue, result.NullableDateOnlyWithValue);
        Assert.Equal(request.NullableDateOnlyDefault, result.NullableDateOnlyDefault);
        Assert.Equal(request.DateTimeOffset, result.DateTimeOffset);
        Assert.Equal(request.DateTimeOffsetDefault, result.DateTimeOffsetDefault);
        Assert.Equal(request.NullableDateTimeOffsetWithValue, result.NullableDateTimeOffsetWithValue);
        Assert.Equal(request.NullableDateTimeOffsetDefault, result.NullableDateTimeOffsetDefault);
        Assert.Equal(request.DateTime, result.DateTime);
        Assert.Equal(request.DateTimeDefault.ToUniversalTime(), result.DateTimeDefault.ToUniversalTime());
        Assert.Equal(request.NullableDateTimeWithValue, result.NullableDateTimeWithValue);
        Assert.Equal(request.NullableDateTimeDefault, result.NullableDateTimeDefault);
        Assert.Equal(request.TimeOnly, result.TimeOnly);
        Assert.Equal(request.TimeOnlyDefault, result.TimeOnlyDefault);
        Assert.Equal(request.NullableTimeOnlyWithValue, result.NullableTimeOnlyWithValue);
        Assert.Equal(request.NullableTimeOnlyDefault, result.NullableTimeOnlyDefault);
        Assert.Equal(request.TimeSpan, result.TimeSpan);
        Assert.Equal(request.TimeSpanDefault, result.TimeSpanDefault);
        Assert.Equal(request.NullableTimeSpanWithValue, result.NullableTimeSpanWithValue);
        Assert.Equal(request.NullableTimeSpanDefault, result.NullableTimeSpanDefault);
    }
}
