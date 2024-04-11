namespace BlazorUtils.EasyApi.Tests.SUT.Contract.ResponseTypes;

[Route("response-type/primitive/integral")]
public class IntegralResponseRequest : IGet<int> { }

[Route("response-type/primitive/nullable-integral")]
public class NullableIntegralResponseRequest : IGet<int?>
{
    [HeaderParam] public bool ExpectValue { get; init; }
}

[Route("response-type/primitive/floating")]
public class FloatingResponseRequest : IGet<float> { }

[Route("response-type/primitive/nullable-floating")]
public class NullableFloatingResponseRequest : IGet<float?>
{
    [HeaderParam] public bool ExpectValue { get; init; }
}

[Route("response-type/primitive/boolean")]
public class BooleanResponseRequest : IGet<bool> { }

[Route("response-type/primitive/nullable-boolean")]
public class NullableBooleanResponseRequest : IGet<bool?>
{
    [HeaderParam] public bool ExpectValue { get; init; }
}

[Route("response-type/primitive/character")]
public class CharacterResponseRequest : IGet<char> { }

[Route("response-type/primitive/nullable-character")]
public class NullableCharacterResponseRequest : IGet<char?>
{
    [HeaderParam] public bool ExpectValue { get; init; }
}
