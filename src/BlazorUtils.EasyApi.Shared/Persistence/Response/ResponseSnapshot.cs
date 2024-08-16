using System.Net;

namespace BlazorUtils.EasyApi.Shared.Persistence.Response;

internal record ResponseSnapshot<ResponseType>(HttpStatusCode StatusCode, ResponseType Response);
