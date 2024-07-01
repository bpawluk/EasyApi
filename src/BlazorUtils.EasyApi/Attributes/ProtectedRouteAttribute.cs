using System;

namespace BlazorUtils.EasyApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ProtectedRouteAttribute : RouteAttribute 
    {
        public string? Policy { get; set; }

        public string? Roles { get; set; }

        public string? AuthenticationSchemes { get; set; }

        public ProtectedRouteAttribute(string value) : base(value) { }
    }
}
