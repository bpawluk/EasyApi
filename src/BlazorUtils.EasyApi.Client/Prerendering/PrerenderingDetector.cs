using BlazorUtils.EasyApi.Shared.Prerendering;

namespace BlazorUtils.EasyApi.Client.Prerendering;

internal class PrerenderingDetector : IPrerenderingDetector
{
    public bool IsPrerendering => false;
}
