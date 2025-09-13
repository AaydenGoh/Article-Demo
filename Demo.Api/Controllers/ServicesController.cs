using Microsoft.AspNetCore.Mvc;
using Demo.Api.Services;

namespace Demo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IConfigurationService _configService;
    private readonly DbService _dbService;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(
        IConfigurationService configService,
        DbService dbService,
        ILogger<ServicesController> logger)
    {
        _configService = configService;
        _dbService = dbService;
        _logger = logger;
    }

    [HttpGet("info")]
    public ActionResult<object> GetServicesInfo()
    {
        _logger.LogInformation("ServicesController.GetServicesInfo called");
        
        var info = new
        {
            Timestamp = DateTime.UtcNow,
            ConfigurationService = _configService.GetServiceInfo(),
            DbService = _dbService.GetServiceInfo(),
            Summary = new
            {
                CacheEnabled = _configService.IsCacheEnabled()
            }
        };

        return Ok(info);
    }

    [HttpGet("configuration")]
    public ActionResult<object> GetConfiguration()
    {
        return Ok(new
        {
            DatabaseConnection = _configService.GetDatabaseConnectionString(),
            MessageBusConnection = _configService.GetMessageBusConnectionString(),
            CacheEnabled = _configService.IsCacheEnabled()
        });
    }
} 