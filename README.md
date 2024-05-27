# EasyApi
Introducing Easy Api – a new way to define and consume Web APIs for Blazor apps. 

## Why would you want to use it?

### 1. Contract-First API development
- Bring order to your development process and gain a clear and consistent API structure.

### 2. Shared data model
- Harness the true power of Blazor with a single data model for your server and client code.  
- Forget about countless DTOs, mapping, versioning, and runtime errors.  
- Welcome EasyApi's type-safe contract shared between both ends of your application.

### 3. Unified logic across all Render Modes
- Stop worrying about where your client code is running.  
- Just call your requests and let EasyApi do the rest.  
- It does not matter if it is Wasm, SSR or prerendering – the job gets done.

### 4. No boilerplate
- Save time and focus on what's important.  
- EasyApi handles both client and server configuration for you.  
- All you need to do is define the contract and handle your requests.

## Sounds good? 
Jump right into practice with [EasyApi Website](https://github.com/bpawluk/EasyApiWebsite) – an application that demonstrates how to set up and use EasyApi.

## Want to know more? 
The path to understanding EasyApi is straightforward and consists of getting to know the three key elements of the library - the contract, the server, and the client apps.

### Contract
EasyApi Contract defines the API endpoints you want to expose, along with the structure of requests and responses supported by the endpoints. It is represented as a set of C# ```classes```, each of which corresponds to a single endpoint.

#### Setup
##### 1. Add a new Class Library Project to your Solution.
##### 2. Reference the [BlazorUtils.EasyApi](https://www.nuget.org/packages/BlazorUtils.EasyApi) NuGet package.
```xml
<PackageReference Include="BlazorUtils.EasyApi" Version="[use the latest version here]" />
```

#### Defining the Contract

##### 1. Create the ```class``` for your endpoint.

```csharp
public class AddComment {}
```

##### 2. Declare the HTTP Method you want to use by implementing one of the predefined ```interfaces```.

```csharp
public class AddComment : IPost {}
```

If the endpoint is to respond with specific data, use a generic version of the ```interface``` and specify the expected response type.  

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

##### 3. Declare the endpoint's route by using the ```RouteAttribute```.

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

##### 4. Define the parameters expected in requests sent to the endpoint.

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
TBD

#### Setup
TBD

### Client
TBD

#### Setup
TBD

## Additional configuration

### Client provider
TBD

### Customized endpoints
TBD
