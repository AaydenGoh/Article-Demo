using Microsoft.Extensions.DependencyInjection;

namespace Demo.Tests;

public static class ServiceCollectionExtensions
{
    public static void Replace<TService>(this IServiceCollection services,
        Func<TService, TService> modify) where TService : class
    {
        var matchedService = services.First(x => x.ServiceType == typeof(TService));
        if (matchedService == null) return;

        var descriptor = ServiceDescriptor.Describe(typeof(TService), provider =>
        {
            TService service = null!;
            if (matchedService.ImplementationType != null)
            {
                var instance = ActivatorUtilities.CreateInstance(provider, matchedService.ImplementationType);
                service = (TService)instance;
            }
            return modify(service);
        }, matchedService.Lifetime);
        services.Remove(matchedService);
        services.Add(descriptor);
    }
}