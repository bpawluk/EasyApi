using System.Net;

namespace BlazorUtils.EasyApi.Shared.Persistence;

internal record ResponseSnapshot<ResponseType>(HttpStatusCode StatusCode, ResponseType Response);
