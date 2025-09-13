namespace Demo.Api.Services;

public interface IConfigurationService
{
    string GetDatabaseConnectionString();
    string GetMessageBusConnectionString();
    bool IsCacheEnabled();
    string GetServiceInfo();
}

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;    
    public string _databaseConnection;
    public string _messageBusConnection;
    private readonly bool _cacheEnabled;

    public ConfigurationService(IConfiguration configuration, ILogger<ConfigurationService> logger)
    {
        _configuration = configuration;
        _databaseConnection = _configuration.GetConnectionString("DefaultDatabase") ?? "default-connection";
        _messageBusConnection = _configuration.GetConnectionString("MessageBus") ?? "default-messagebus";
        _cacheEnabled = _configuration.GetValue<bool>("Cache:Enabled");
    }

    public string GetDatabaseConnectionString()
    {
        return _databaseConnection;
    }

    public string GetMessageBusConnectionString()
    {
        return _messageBusConnection;
    }

    public bool IsCacheEnabled()
    {
        return _cacheEnabled;
    }

    public string GetServiceInfo()
    {
        return $"ConfigurationService[Database:{_databaseConnection}, MessageBus:{_messageBusConnection}, Cache:{_cacheEnabled}]";
    }
}

public class DbService
{
    private readonly IConfiguration _configuration;
    public string _databaseConnection;
    public string _messageBusConnection;
    private readonly bool _cacheEnabled;

    public DbService(IConfiguration configuration, ILogger<ConfigurationService> logger)
    {
        _configuration = configuration;
        _databaseConnection = _configuration.GetConnectionString("DefaultDatabase") ?? "default-connection";
        _messageBusConnection = _configuration.GetConnectionString("MessageBus") ?? "default-messagebus";
        _cacheEnabled = _configuration.GetValue<bool>("Cache:Enabled");
    }

    public string GetDatabaseConnectionString()
    {
        return _databaseConnection;
    }

    public string GetMessageBusConnectionString()
    {
        return _messageBusConnection;
    }

    public bool IsCacheEnabled()
    {
        return _cacheEnabled;
    }

    public string GetServiceInfo()
    {
        return $"ConfigurationService[Database:{_databaseConnection}, MessageBus:{_messageBusConnection}, Cache:{_cacheEnabled}]";
    }
}