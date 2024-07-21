using BlazorUtils.EasyApi.Shared.Rendering;

namespace BlazorUtils.EasyApi.Client.Rendering;

internal class InteractivityDetector : IInteractivityDetector
{
    public bool IsInteractiveWebAssembly => true;

    public bool IsInteractiveServer => false;
}
