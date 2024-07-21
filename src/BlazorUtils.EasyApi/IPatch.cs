namespace BlazorUtils.EasyApi
{
    public interface IPatch : IRequest { }

    public interface IPatch<out Response> : IRequest<Response> { }
}
