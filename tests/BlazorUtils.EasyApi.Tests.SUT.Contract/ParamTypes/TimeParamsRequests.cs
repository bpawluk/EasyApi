namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;

[Route("param-type/time")]
public class TimeParamsRequest : IPost<TimeParamsRequest.Response>
{
    [HeaderParam] public DateOnly DateOnly { get; init; }
    [HeaderParam] public DateOnly DateOnlyDefault { get; init; }
    [HeaderParam] public DateOnly? NullableDateOnlyWithValue { get; init; }
    [HeaderParam] public DateOnly? NullableDateOnlyDefault { get; init; }

    [QueryStringParam] public DateTimeOffset DateTimeOffset { get; init; }
    [QueryStringParam] public DateTimeOffset DateTimeOffsetDefault { get; init; }
    [QueryStringParam] public DateTimeOffset? NullableDateTimeOffsetWithValue { get; init; }
    [QueryStringParam] public DateTimeOffset? NullableDateTimeOffsetDefault { get; init; }

    [BodyParam] public DateTime DateTime { get; init; }
    [BodyParam] public DateTime DateTimeDefault { get; init; }
    [BodyParam] public DateTime? NullableDateTimeWithValue { get; init; }
    [BodyParam] public DateTime? NullableDateTimeDefault { get; init; }

    [HeaderParam] public TimeOnly TimeOnly { get; init; }
    [HeaderParam] public TimeOnly TimeOnlyDefault { get; init; }
    [HeaderParam] public TimeOnly? NullableTimeOnlyWithValue { get; init; }
    [HeaderParam] public TimeOnly? NullableTimeOnlyDefault { get; init; }

    [QueryStringParam] public TimeSpan TimeSpan { get; init; }
    [QueryStringParam] public TimeSpan TimeSpanDefault { get; init; }
    [QueryStringParam] public TimeSpan? NullableTimeSpanWithValue { get; init; }
    [QueryStringParam] public TimeSpan? NullableTimeSpanDefault { get; init; }

    public class Response
    {
        public DateOnly DateOnly { get; init; }
        public DateOnly DateOnlyDefault { get; init; }
        public DateOnly? NullableDateOnlyWithValue { get; init; }
        public DateOnly? NullableDateOnlyDefault { get; init; }

        public DateTimeOffset DateTimeOffset { get; init; }
        public DateTimeOffset DateTimeOffsetDefault { get; init; }
        public DateTimeOffset? NullableDateTimeOffsetWithValue { get; init; }
        public DateTimeOffset? NullableDateTimeOffsetDefault { get; init; }

        public DateTime DateTime { get; init; }
        public DateTime DateTimeDefault { get; init; }
        public DateTime? NullableDateTimeWithValue { get; init; }
        public DateTime? NullableDateTimeDefault { get; init; }

        public TimeOnly TimeOnly { get; init; }
        public TimeOnly TimeOnlyDefault { get; init; }
        public TimeOnly? NullableTimeOnlyWithValue { get; init; }
        public TimeOnly? NullableTimeOnlyDefault { get; init; }

        public TimeSpan TimeSpan { get; init; }
        public TimeSpan TimeSpanDefault { get; init; }
        public TimeSpan? NullableTimeSpanWithValue { get; init; }
        public TimeSpan? NullableTimeSpanDefault { get; init; }
    }
}
