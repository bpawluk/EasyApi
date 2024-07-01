namespace BlazorUtils.EasyApi
{
    public interface IPut : IRequest { }

    public interface IPut<out Response> : IRequest<Response> { }
}
