using Demo.Api.Services;
using Microsoft.Extensions.Logging;

namespace Demo.Tests.Mocks;

public class MockConfigurationService : IConfigurationService
{
    private readonly ILogger<MockConfigurationService> _logger;

    public MockConfigurationService(ILogger<MockConfigurationService> logger)
    {
        _logger = logger;
        _logger.LogInformation("MockConfigurationService constructor called");
    }

    public string GetDatabaseConnectionString()
    {
        return "mock-database-connection";
    }

    public string GetMessageBusConnectionString()
    {
        return "mock-messagebus-connection";
    }

    public bool IsCacheEnabled()
    {
        return true;
    }

    public string GetServiceInfo()
    {
        return "MockConfigurationService[Database:mock-database-connection, MessageBus:mock-messagebus-connection, Cache:True]";
    }
}