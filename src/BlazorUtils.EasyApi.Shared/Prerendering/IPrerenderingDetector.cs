namespace BlazorUtils.EasyApi.Shared.Prerendering;

internal interface IPrerenderingDetector
{
    bool IsPrerendering { get; }
}
