using Demo.Api.Services;
using Demo.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Demo.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly List<string> _executionLog = [];
    public List<string> GetExecutionLog() => _executionLog.ToList();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _executionLog.Add("ConfigureWebHost Start");
        builder.ConfigureTestServices(services =>
        {
            _executionLog.Add("ConfigureTestServices Start");
            #region Common Service Registration
            //When you have multiple registrations for the same service type, the last registration wins            
            services.AddSingleton<IConfigurationService, MockConfigurationService>();
            //or
            //This would explicitly replace the existing registration
            services.Replace(new ServiceDescriptor(typeof(IConfigurationService), typeof(MockConfigurationService), ServiceLifetime.Singleton));
            #endregion

            #region Partial Service Modification
            services.Replace<DbService>(options =>
            {
                options._databaseConnection = "test-postgresql-connection";
                return options;
            });
            #endregion
            _executionLog.Add("ConfigureTestServices End");
        });

        builder.UseEnvironment("Test");
        _executionLog.Add("ConfigureWebHost End");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _executionLog.Add("CreateHost Start");

        var host = builder.Build();
        _executionLog.Add("Host built, application starting");

        host.Start();
        _executionLog.Add("Host started");
        _executionLog.Add("CreateHost End");

        return host;
    }
}