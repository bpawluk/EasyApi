namespace BlazorUtils.EasyApi
{
    public interface IRequest { }

    public interface IRequest<out Response> : IRequest { }
}
