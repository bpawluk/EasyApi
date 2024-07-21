using System;

namespace BlazorUtils.EasyApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class RouteAttribute : Attribute 
    {
        public string Value { get; }

        public RouteAttribute(string value)
        {
            Value = value;
        }
    }
}
