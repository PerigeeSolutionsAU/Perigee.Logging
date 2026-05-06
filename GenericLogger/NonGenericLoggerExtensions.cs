using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class NonGenericLoggerExtensions
{
    public static IServiceCollection AddNonGenericLoggerSupport(this IServiceCollection services)
    {
        // Find all standard type-based registrations (exclude factories and open generics)
        // that have at least one constructor accepting a non-generic ILogger
        var descriptorsWithLogger = services
            .Where(d => d.ImplementationType != null && !d.ImplementationType.IsGenericTypeDefinition)
            .Where(d => d.ImplementationType!.GetConstructors()
                .Any(c => c.GetParameters().Any(p => p.ParameterType == typeof(ILogger))))
            .ToList();

        foreach (var descriptor in descriptorsWithLogger)
        {
            // Remove the standard registration
            services.Remove(descriptor);

            // Pre-compile the factory for maximum performance using ActivatorUtilities.
            // We tell it to expect one explicit argument of type ILogger.
            var objectFactory = ActivatorUtilities.CreateFactory(
                descriptor.ImplementationType!, 
                new[] { typeof(ILogger) });

            // Replace with a factory registration that respects the original lifetime
            services.Add(new ServiceDescriptor(
                descriptor.ServiceType,
                provider =>
                {
                    // Create the properly categorised logger based on the consuming type
                    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger(descriptor.ImplementationType!);
                    
                    // Create the instance, passing our custom logger. 
                    // The DI container handles resolving all other dependencies.
                    return objectFactory(provider, new object[] { logger });
                },
                descriptor.Lifetime));
        }

        return services;
    }
}
