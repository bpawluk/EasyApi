namespace BlazorUtils.EasyApi.Shared.Persistence;

internal record PersistedResponse<ResponseType>
{
    public HttpResult<ResponseType> Value { get; }

    public bool IsSticky { get; }

    private PersistedResponse(HttpResult<ResponseType> value, bool isSticky)
    {
        Value = value;
        IsSticky = isSticky;
    }

    public static PersistedResponse<ResponseType> Create(HttpResult<ResponseType> value) => new(value, false);

    public static PersistedResponse<ResponseType> Sticky(HttpResult<ResponseType> value) => new(value, true);
}
