namespace BlazorUtils.EasyApi;

public interface IGet : IRequest { }

public interface IGet<out Response> : IRequest<Response> { }