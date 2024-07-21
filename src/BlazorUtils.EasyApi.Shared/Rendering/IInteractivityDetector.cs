namespace BlazorUtils.EasyApi.Shared.Rendering;

internal interface IInteractivityDetector
{
    public bool IsInteractive => IsInteractiveWebAssembly || IsInteractiveServer;

    bool IsInteractiveWebAssembly { get; }

    bool IsInteractiveServer { get; }
}
