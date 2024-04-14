using System;

namespace BlazorUtils.EasyApi.Shared.Exceptions;

internal class SetupException : Exception 
{
    public SetupException(string message) : base(message) { }

    public SetupException(string message, Exception innerException) : base(message, innerException) { }
}
