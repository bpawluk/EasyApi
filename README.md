# EasyApi
Introducing Easy Api – a new way to define and consume Web APIs for Blazor apps. 

## Why would you want to use it?

### 1. Contract-First API development
  Bring order to your development process and gain a clear and consistent API structure.

### 2. Shared data model
  Harness the true power of Blazor with a single data model for your server and client code.  
  Forget about countless DTOs, mapping, versioning, and runtime errors.  
  Welcome EasyApi's type-safe contract shared between both ends of your application.

### 3. Unified logic across all Render Modes
  Stop worrying about where your client code is running.  
  Just call your requests and let EasyApi do the rest.  
  It does not matter if it is Wasm, SSR or prerendering – the job gets done.

### 4. No boilerplate
  Save time and focus on what's important.  
  EasyApi handles both client and server configuration for you.  
  All you need to do is define the contract and handle your requests.

## Sounds good? 
Jump right into practice with [EasyApi Website](https://github.com/bpawluk/EasyApiWebsite) – an application that demonstrates how to set up and use EasyApi.

## Want to know more? 
The path to understanding EasyApi is straightforward and consists of getting to know the three key elements of the library - the contract, the server, and the client apps.

### Contract
EasyApi Contract defines the structure of your API requests. It is represented as a set of C# ```classes```, each of which:
- must implement one of the predefined ```interfaces``` that declare the HTTP method to use:
  - ```IHead```, 
  - ```IGet``` or ```IGet<ResponseType>``` 
  - ```IPost``` or ```IPost<ResponseType>```, 
  - ```IPut``` or ```IPut<ResponseType>```, 
  - ```IPatch``` or ```IPatch<ResponseType>```, 
  - ```IDelete``` or ```IDelete<ResponseType>```;

- should be marked with one of the available ```attributes``` that declare the API endpoint route:
  - ```RouteAttribute``` - for publicly available endpoints,
  - ```ProtectedRouteAttribute``` - for endpoints requiring authorization;

    > :warning: BEWARE  
    > Authorization is only used to protect the API endpoint itself. Do not base your client security on it, as requests are not authorized during pre-rendering and server-side rendering scenarios. Use [Blazor authorization measures](https://learn.microsoft.com/en-us/aspnet/core/blazor/security/#authorization) instead. 

- can contain properties marked with one of the available ```attributes``` that declare the request paramaters:
  - ```BodyParamAttribute``` - sent in the body of a HTTP request for methods that support it, 
  - ```HeaderParamAttribute``` - sent as a HTTP header, 
  - ```QueryStringParamAttribute``` - sent as an argument appended to the endpoint URL, 
  - ```RouteParamAttribute``` - sent within the endpoint URL itself.

    > ℹ️ NOTE  
    > The route must contain a matching placeholder in the following format: ```{PropertyName}```.

#### Setup
TBD

### Server 
TBD

### Client
TBD

## Additional configuration

### Client provider
TBD

### Customized endpoints
TBD
