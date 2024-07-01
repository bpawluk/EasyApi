namespace BlazorUtils.EasyApi.Client.Setup;

public class PersistenceOptions
{
    internal bool UsingPrerenderingState { get; private set; }

    private PersistenceOptions() { }

    public static PersistenceOptions With => new();

    public PersistenceOptions PrerenderingState()
    {
        UsingPrerenderingState = true;
        return this;
    }
}
