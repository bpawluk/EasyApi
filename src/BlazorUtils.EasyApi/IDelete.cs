namespace BlazorUtils.EasyApi;

public interface IDelete : IRequest { }

public interface IDelete<out Response> : IRequest<Response> { }
