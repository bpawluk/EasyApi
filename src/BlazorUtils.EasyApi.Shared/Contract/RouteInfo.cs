namespace BlazorUtils.EasyApi.Shared.Contract;

internal class RouteInfo
{
    public string Value { get; }

    public AuthorizationInfo Authorization { get; }

    public static RouteInfo Default => new(string.Empty);

    private RouteInfo(string value)
    {
        Value = value;
        Authorization = new(false);
    }

    public RouteInfo(RouteAttribute routeAttribute) : this(routeAttribute.Value) { }

    public RouteInfo(ProtectedRouteAttribute protectedRouteAttribute) : this(protectedRouteAttribute.Value)
    {
        Authorization = new(true)
        {
            Policy = protectedRouteAttribute.Policy,
            Roles = protectedRouteAttribute.Roles,
            AuthenticationSchemes = protectedRouteAttribute.AuthenticationSchemes
        };
    }

    public class AuthorizationInfo
    {
        public bool Authorize { get; }

        public string? Policy { get; init; }

        public string? Roles { get; init; }

        public string? AuthenticationSchemes { get; init; }

        public AuthorizationInfo(bool authorize)
        {
            Authorize = authorize;
        }
    }
}
