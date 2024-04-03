using BlazorUtils.EasyApi.Tests.SUT.Contract.ParamTypes;
using BlazorUtils.EasyApi.Tests.SUT.Server;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorUtils.EasyApi.Tests.ParamTypes;

public class EnumParamsTests(WebApplicationFactory<Program> factory) : TestsBase(factory)
{
    [Fact]
    public async Task Request_WithEnumParams()
    {
        var request = new EnumParamsRequest()
        {
            Enum = Season.Summer,
            EnumDefault = default,
            NullableEnumWithValue = Season.Autumn,
            NullableEnumDefault = null
        };

        var result = await CallHttp<EnumParamsRequest, EnumParamsRequest.Response>(request);

        Assert.Equal(request.Enum, result.Enum);
        Assert.Equal(request.EnumDefault, result.EnumDefault);
        Assert.Equal(request.NullableEnumWithValue, result.NullableEnumWithValue);
        Assert.Equal(request.NullableEnumDefault, result.NullableEnumDefault);
    }
}
