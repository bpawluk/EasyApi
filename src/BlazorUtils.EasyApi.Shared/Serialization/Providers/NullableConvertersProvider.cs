using BlazorUtils.EasyApi.Shared.Reflection;
using BlazorUtils.EasyApi.Shared.Serialization.Converters.Other;
using System;
using System.Linq;

namespace BlazorUtils.EasyApi.Shared.Serialization.Providers;

internal class NullableConvertersProvider : IConvertersProvider
{
    private readonly IConvertersProvider _convertersProvider;

    public NullableConvertersProvider(IConvertersProvider convertersProvider)
    {
        _convertersProvider = convertersProvider;
    }

    public IParamConverter<T>? Get<T>()
    {
        var requestedType = typeof(T);
        if (requestedType.IsGenericType && requestedType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return typeof(NullableParamConverter<>)
                .Apply(requestedType.GenericTypeArguments.Single())
                .Invoke(nameof(NullableParamConverter<int>.GetInstance), _convertersProvider) as IParamConverter<T>;
        }
        return null!;
    }
}
