# EasyApi
Introducing Easy Api – a new way to define and consume Web APIs for Blazor apps. 

## Why would you want to use it?

**✅ Contract-First API development**
- Bring order to your development process and gain a clear and consistent API structure.

**✅ Shared data model**
- Harness the true power of Blazor with a single data model for your server and client code.  
- Forget about countless DTOs, mapping, versioning, and runtime errors.  
- Welcome EasyApi's type-safe contract shared between both ends of your application.

**✅ Unified logic across all Render Modes**
- Stop worrying about where your client code is running.  
- Just call your requests and let EasyApi do the rest.  
- It does not matter if it is Wasm, SSR or prerendering – the job gets done.

**✅ No boilerplate**
- Save time and focus on what's important.  
- EasyApi handles both client and server configuration for you.  
- All you need to do is define the contract and handle your requests.

## Sounds good? 
Jump right into practice with [EasyApi Website](https://github.com/bpawluk/EasyApiWebsite) – an application that demonstrates how to set up and use EasyApi.

## Want to know more? 
The path to understanding EasyApi is straightforward and consists of getting to know the three key elements of the library - the contract, the server, and the client apps.

### Contract
EasyApi Contract defines the API endpoints you want to expose and consume. Each endpoint is represented as a C# ```class``` that primarily defines the structure of incoming requests and the type of returned responses along with the HTTP medium details.

#### Setup

**1️⃣ Add a new Class Library Project to your Solution.**

**2️⃣ Reference the [BlazorUtils.EasyApi](https://www.nuget.org/packages/BlazorUtils.EasyApi) NuGet package.**

```xml
<PackageReference Include="BlazorUtils.EasyApi" Version="[use the latest version here]" />
```

#### Defining the Contract

**1️⃣ Create a ```class``` for your requests.**

```csharp
public class AddComment {}
```

**2️⃣ Declare the HTTP Method you want to use by implementing one of the predefined ```interfaces```.**

```csharp
public class AddComment : IPost {}
```

If the API endpoint is to respond with specific data, use a generic version of the ```interface``` and specify the expected response type.  

```csharp
public class AddComment : IPost<Guid> {}
```

The full list of available ```interfaces``` includes: 
  - ```IHead```, 
  - ```IGet``` and ```IGet<ResponseType>```,
  - ```IPost``` and ```IPost<ResponseType>```, 
  - ```IPut``` and ```IPut<ResponseType>```, 
  - ```IPatch``` and ```IPatch<ResponseType>```, 
  - ```IDelete``` and ```IDelete<ResponseType>```.

**3️⃣ Declare the API endpoint's route by using the ```RouteAttribute```.**

```csharp
[Route("api/articles/{ArticleID}/comments")]
public class AddComment : IPost<Guid> {}
```

Use ```ProtectedRouteAttribute``` if the endpoint requires authorization.

```csharp
[ProtectedRoute("api/articles/{ArticleID}/comments")]
public class AddComment : IPost<Guid> {}
```

> [!CAUTION]
> Authorization is used only to protect the API endpoint. EasyApi requests are not authorized during pre-rendering and server-side rendering scenarios. Use [Blazor authorization measures](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/#authorization) to protect your Client app. 

**4️⃣ Define the parameters that make up the request structure.**

```csharp
[ProtectedRoute("api/articles/{ArticleID}/comments")]
public class AddComment : IPost<Guid>
{
    [RouteParam]
    public Guid ArticleID { get; init; }

    [BodyParam]
    public string Author { get; init; } = default!;

    [BodyParam]
    public string Content { get; init; } = default!;
}
```

You can specify a different way of sending values for each of the parameters by using one of the predefined ```attributes```: 
- ```BodyParamAttribute``` - value sent in the body of a HTTP request (if the HTTP Method supports it), 
- ```HeaderParamAttribute``` - value sent as a HTTP Header, 
- ```QueryStringParamAttribute``` - value sent as an argument appended to the requested URL, 
- ```RouteParamAttribute``` - value sent within the requested URL itself.

> [!NOTE]  
> When using route parameters, the route must contain matching ```{PropertyName}``` placeholders.

### Server 
The responsibility of the EasyApi Server app is to handle all requests defined in the Contract.

#### Setup

**1️⃣ You should start with a ```Microsoft.NET.Sdk.Web``` SDK Project that is the Server for your application.**

**2️⃣ Reference the [BlazorUtils.EasyApi.Server](https://www.nuget.org/packages/BlazorUtils.EasyApi.Server) NuGet package.**

```xml
<PackageReference Include="BlazorUtils.EasyApi.Server" Version="[use the latest version here]" />
```

**3️⃣ Register EasyApi services.**

```csharp
using BlazorUtils.EasyApi;
using BlazorUtils.EasyApi.Server;

var contractAssembly = typeof(AddComment).Assembly;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithServer();
```

**4️⃣ Map EasyApi endpoints.**
```csharp
var app = builder.Build();
// [...]
app.MapRequests();
```

#### Handling requests

**1️⃣ Create the request handler ```class```.**

```csharp
internal class AddCommentHandler : IHandle<AddComment, Guid> { }
```

**2️⃣ Implement the ```IHandle``` ```interface```.**

```csharp
internal class AddCommentHandler(ICommentsRepository CommentsRepository) : IHandle<AddComment, Guid>
{
    public async Task<HttpResult<Guid>> Handle(AddComment request, CancellationToken cancellationToken)
    {
        var newComment = new Comment(request);
        var newCommentID = await CommentsRepository.Add(newComment, cancellationToken);
        return HttpResult<Guid>.Created(newCommentID);
    }
}
```

### Client
The EasyApi Client app creates requests defined in the Contract and sends them to the Server for processing.

#### Setup

**1️⃣ You should start with a ```Microsoft.NET.Sdk.BlazorWebAssembly``` SDK Project that is your Client application.**

**2️⃣ Reference the [BlazorUtils.EasyApi.Client](https://www.nuget.org/packages/BlazorUtils.EasyApi.Client) NuGet package.**

```xml
<PackageReference Include="BlazorUtils.EasyApi.Client" Version="[use the latest version here]" />
```

**3️⃣ Register EasyApi services.**

```csharp
using BlazorUtils.EasyApi;
using BlazorUtils.EasyApi.Client;

var contractAssembly = typeof(AddComment).Assembly;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithClient();
```

**4️⃣ Setup the ```HttpClient```.**

Simply register it as a service,

```csharp
builder.Services.AddScoped(provider => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
```

or [configure a provider](#http-client-provider) for more control over the ```HttpClient```.

#### Sending requests

**1️⃣ Inject the caller of the endpoint you want to use.**

```csharp
@inject ICall<AddComment, Guid> AddComment
```

**2️⃣ Create the request.**

```csharp
var request = new AddPost()
{
    ArticleID = Article.ID,
    Author = "EasyApi Enjoyer",
    Content = "This is very easy!"
};
```

**3️⃣ Call the API endpoint.**

Use the ```Call``` method to simply get the response,

```csharp
Guid newCommentID = await AddComment.Call(request);
```

or turn to the ```CallHttp``` method if you need to access the ```HttpResult```.

```csharp
HttpResult<Guid> result = await AddComment.CallHttp(request);
if (result.StatusCode == HttpStatusCode.Created)
{
    Guid newCommentID = result.Response!;
}
```

## Additional configuration

### Response Persistence
TODO

**1️⃣ Configure one of the available persistence options.**
- [Prerendered Response Persistence](#prerendered-response-persistence)
- [In-memory Response Persistence](#in-memory-response-persistence)

**2️⃣ TODO**

```csharp
@inject IPersistentCall<GetComments, IEnumerable<Comment>> GetComments
```

**3️⃣ TODO**

```csharp
var comments = await GetComments.Call("some-request-identifier", request);
```

#### Prerendered Response Persistence
TODO

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithServer()
    .Using<PrerenderedResponsePersistence>();
```

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithClient()
    .Using<PrerenderedResponsePersistence>();
```

#### In-memory Response Persistence
TODO

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .With[Client/Server]()
    .Using<InMemoryResponsePersistence>();
```

> [!NOTE]  
> TODO

```csharp
internal class CustomInMemoryResponsePersistence : IInMemoryResponsePersistence
{
    public InMemoryResponsePersistenceOptions Configure(IRequest request)
    {
        if (request response should be persisted)
        {
            return new() { IsEnabled = true };
        }
        return new() { IsEnabled = false };
    }
}
```

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .With[Client/Server]()
    .Using<CustomInMemoryResponsePersistence>();
```

### Client extensions

#### HTTP client provider
By default, EasyApi uses the ```HttpClient``` registered in the service collection of your Client app. Whenever you need more control over the ```HttpClient``` creation or need to manage different ```HttpClients``` for different API requests, you should set up your own provider.

**1️⃣ Implement the ```IHttpClientProvider``` ```interface```.**

```csharp
internal class HttpClientProvider : IHttpClientProvider
{
    public HttpClient GetClient(IRequest request)
    {
        // your logic goes here
    }
}
```

**2️⃣ Register the HTTP client provider for your Client app.**

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithClient()
    .Using<HttpClientProvider>();
```

### Server extensions

#### API endpoints customization
EasyApi handles the basic API endpoint configuration for you. For advanced scenarios that require more configuration, you should set up your own API endpoint customization.

**1️⃣ Implement the ```IEndpointsCustomization``` ```interface```.**

```csharp
internal class EndpointsCustomization : IEndpointsCustomization
{
    public void Customize<Request>(RouteHandlerBuilder builder)
    {
        // your logic goes here
    }
}
```

**2️⃣ Register the endpoints customization for your Server app.**

```csharp
builder.Services
    .AddEasyApi()
    .WithContract(contractAssembly)
    .WithServer()
    .Using<EndpointsCustomization>();
```

## Change Log

### v1.0.0
- .NET 8 upgrade
- Introduced IPersistentCall
- Introduced PrerenderedResponsePersistence
- Introduced InMemoryResponsePersistence

### v0.5.1
- Fixed incorrect HTTP method mapping for IPut requests.
- Fixed null string parameters being deserialized as empty strings.
- Query String and Header params are now skipped when sending null values.

### v0.5.0
- Baseline version of the library.
