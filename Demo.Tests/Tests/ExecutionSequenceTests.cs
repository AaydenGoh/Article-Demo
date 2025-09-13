using Xunit;
using Xunit.Abstractions;

namespace Demo.Tests.Tests;

public class ExecutionSequenceTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory = factory;
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void CustomWebAppFactory_ShouldConfigureTestServiceRanAfterConfigureWebHost()
    {
        _ = _factory.CreateClient();

        var executionLogs = _factory.GetExecutionLog();

        _output.WriteLine("=== WAF Execution Sequences ===");
        foreach (var log in executionLogs)
        {
            _output.WriteLine($"  {log}");
        }

        var configureTestServicesIndex = executionLogs.IndexOf("ConfigureTestServices Start");
        var configurationWebHostIndex = executionLogs.IndexOf("ConfigureWebHost End");

        Assert.True(configurationWebHostIndex < configureTestServicesIndex,
            "Congfiguration Test Service run after configure web host");
    }
}
