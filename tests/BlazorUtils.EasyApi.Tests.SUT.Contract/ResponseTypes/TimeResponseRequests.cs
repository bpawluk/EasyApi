namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

[Route("response-type/time/date-only")]
public class DateOnlyResponseRequest : IGet<DateOnly> 
{
    [HeaderParam] public DateOnly ExpectedResponse { get; init; }
}

[Route("response-type/time/nullable-date-only")]
public class NullableDateOnlyResponseRequest : IGet<DateOnly?>
{
    [HeaderParam] public DateOnly? ExpectedResponse { get; init; }
}

[Route("response-type/time/date-time-offset")]
public class DateTimeOffsetResponseRequest : IGet<DateTimeOffset>
{
    [HeaderParam] public DateTimeOffset ExpectedResponse { get; init; }
}

[Route("response-type/time/nullable-date-time-offset")]
public class NullableDateTimeOffsetResponseRequest : IGet<DateTimeOffset?>
{
    [HeaderParam] public DateTimeOffset? ExpectedResponse { get; init; }
}

[Route("response-type/time/date-time")]
public class DateTimeResponseRequest : IGet<DateTime>
{
    [HeaderParam] public DateTime ExpectedResponse { get; init; }
}

[Route("response-type/time/nullable-date-time")]
public class NullableDateTimeResponseRequest : IGet<DateTime?>
{
    [HeaderParam] public DateTime? ExpectedResponse { get; init; }
}

[Route("response-type/time/time-only")]
public class TimeOnlyResponseRequest : IGet<TimeOnly>
{
    [HeaderParam] public TimeOnly ExpectedResponse { get; init; }
}

[Route("response-type/time/nullable-time-only")]
public class NullableTimeOnlyResponseRequest : IGet<TimeOnly?>
{
    [HeaderParam] public TimeOnly? ExpectedResponse { get; init; }
}

[Route("response-type/time/time-span")]
public class TimeSpanResponseRequest : IGet<TimeSpan>
{
    [HeaderParam] public TimeSpan ExpectedResponse { get; init; }
}

[Route("response-type/time/nullable-time-span")]
public class NullableTimeSpanResponseRequest : IGet<TimeSpan?>
{
    [HeaderParam] public TimeSpan? ExpectedResponse { get; init; }
}
