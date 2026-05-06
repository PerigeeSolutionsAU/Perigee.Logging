using Microsoft.Extensions.Logging;

namespace ExampleConsoleApp;


public class App
{
    private readonly ILogger _logger;
    private readonly SomeService _someService;

    public App(ILogger logger, SomeService someService)
    {
        _logger = logger;
        _someService = someService;
    }

    public Task Run()
    {
        _logger.LogInformation("Hello from App! The logger category should be App.");
        _someService.Snoogans();
        _logger.LogTrace("Test Trace");
        _logger.LogDebug("Test Debug");
        _logger.LogInformation("Test Information");
        _logger.LogWarning("Test Warning");
        _logger.LogError("Test Error");
        _logger.LogCritical("Test Critical");
        return Task.CompletedTask; 
        
    }

}
