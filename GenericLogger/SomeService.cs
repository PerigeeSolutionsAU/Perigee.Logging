using Microsoft.Extensions.Logging;

namespace GenericLogger;

public class SomeService
{
    private readonly ILogger _logger;
    public SomeService(ILogger logger)
    {
        _logger = logger;
    }

    public void Snoogans()
    {
        _logger.LogInformation("Snootch to the nootch");
    }
}
