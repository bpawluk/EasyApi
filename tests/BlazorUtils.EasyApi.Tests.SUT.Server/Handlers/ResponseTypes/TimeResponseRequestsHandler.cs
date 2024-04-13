﻿using BlazorUtils.EasyApi.Server;
using BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

namespace BlazorUtils.EasyApi.Tests.SUT.Server.Handlers.ResponseTypes;

internal class TimeResponseRequestsHandler
    : HandlerBase
    , IHandle<DateOnlyResponseRequest, DateOnly>
    , IHandle<NullableDateOnlyResponseRequest, DateOnly?>
    , IHandle<DateTimeOffsetResponseRequest, DateTimeOffset>
    , IHandle<NullableDateTimeOffsetResponseRequest, DateTimeOffset?>
    , IHandle<DateTimeResponseRequest, DateTime>
    , IHandle<NullableDateTimeResponseRequest, DateTime?>
    , IHandle<TimeOnlyResponseRequest, TimeOnly>
    , IHandle<NullableTimeOnlyResponseRequest, TimeOnly?>
    , IHandle<TimeSpanResponseRequest, TimeSpan>
    , IHandle<NullableTimeSpanResponseRequest, TimeSpan?>
{
    public Task<HttpResult<DateOnly>> Handle(DateOnlyResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateOnly>.Ok(request.ExpectedResponse));

    public Task<HttpResult<DateOnly?>> Handle(NullableDateOnlyResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateOnly?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<DateTimeOffset>> Handle(DateTimeOffsetResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateTimeOffset>.Ok(request.ExpectedResponse));

    public Task<HttpResult<DateTimeOffset?>> Handle(NullableDateTimeOffsetResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateTimeOffset?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<DateTime>> Handle(DateTimeResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateTime>.Ok(request.ExpectedResponse));

    public Task<HttpResult<DateTime?>> Handle(NullableDateTimeResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<DateTime?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<TimeOnly>> Handle(TimeOnlyResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<TimeOnly>.Ok(request.ExpectedResponse));

    public Task<HttpResult<TimeOnly?>> Handle(NullableTimeOnlyResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<TimeOnly?>.Ok(request.ExpectedResponse));

    public Task<HttpResult<TimeSpan>> Handle(TimeSpanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<TimeSpan>.Ok(request.ExpectedResponse));

    public Task<HttpResult<TimeSpan?>> Handle(NullableTimeSpanResponseRequest request, CancellationToken cancellationToken)
        => Task.FromResult(HttpResult<TimeSpan?>.Ok(request.ExpectedResponse));
}