namespace BlazorUtils.EasyApi.UnitTests.SerializationTests;

[Route(nameof(SerializationSetupTestsRequest))]
public record SerializationSetupTestsRequest : IGet<SerializationSetupTestsRequest.Response>
{
    public record Response(
        Guid Id,
        DateTime Time,
        string Text,
        int[] Numbers,
        Flag[] Flags
    );

    public record Flag(
        Guid FlagId,
        bool? FlagValue
    );
}
