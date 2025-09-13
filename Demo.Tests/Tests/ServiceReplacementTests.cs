using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Tests.Tests;

public class ServiceReplacementTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory = factory;
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void CustomWebAppFactory_CommonApproachServiceReplacement()
    {
        var client = _factory.CreateClient();

        var executionLogs = _factory.GetExecutionLog();

        _output.WriteLine("=== WAF Execution Sequences ===");
        foreach (var log in executionLogs)
        {
            _output.WriteLine($"  {log}");
        }

        var response = client.GetAsync("/api/services/info").Result;
        response.EnsureSuccessStatusCode();

        var content = response.Content.ReadAsStringAsync().Result;
        var servicesInfo = JsonSerializer.Deserialize<JsonElement>(content);

        var configServiceInfo = servicesInfo.GetProperty("configurationService").GetString();

        _output.WriteLine("=== Service Replace Results ===");
        _output.WriteLine($"  ConfigurationService: {configServiceInfo}");

        Assert.Contains("mock-database-connection", configServiceInfo);
    }

    [Fact]
    public void CustomWebAppFactory_IServiceProvider_ServiceReplacement()
    {
        var client = _factory.CreateClient();

        var executionLogs = _factory.GetExecutionLog();

        _output.WriteLine("=== WAF Execution Sequences ===");
        foreach (var log in executionLogs)
        {
            _output.WriteLine($"  {log}");
        }

        var response = client.GetAsync("/api/services/info").Result;
        response.EnsureSuccessStatusCode();

        var content = response.Content.ReadAsStringAsync().Result;
        var servicesInfo = JsonSerializer.Deserialize<JsonElement>(content);

        var configServiceInfo = servicesInfo.GetProperty("dbService").GetString();

        _output.WriteLine("=== Service Replace Results ===");
        _output.WriteLine($"  DbService: {configServiceInfo}");

        Assert.Contains("test-postgresql-connection", configServiceInfo);
    }
}
