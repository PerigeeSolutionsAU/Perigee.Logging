# Perigee.Logging

This library contains stuff I like to use for logging with .NET projects

## Non Generic ILogger

AddNonGenericLoggerSupport() enables the ability to specify ILoger rathen than ILogger< class_name > which I find overly verbose and tedious.

### Usage

To enable the feature, where you register DI services call

```csharp
builder.Services.AddNonGenericLoggerSupport();
```

then when you need the ILogger injected do this:

```csharp
public class App
{
    private readonly ILogger _logger;
    private readonly SomeService _someService;

    public App(ILogger logger, SomeService someService)
    {
        _logger = logger;
        _someService = someService;
    }

    .
    .
    .

}
```
