namespace BlazorUtils.EasyApi
{
    public interface IPost : IRequest { }

    public interface IPost<out Response> : IRequest<Response> { }
}
