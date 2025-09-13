# Article-Demo

This repository contains a practical demonstration of **ASP.NET Core WebApplicationFactory testing patterns** and **service replacement techniques**. The demo showcases:

- **Custom WebApplicationFactory Implementation**: A `CustomWebApplicationFactory` that extends the base `WebApplicationFactory` to demonstrate execution sequence logging and service replacement
- **Service Replacement Strategies**: Two different approaches for replacing services in integration tests:
  - Interface-based replacement (`IConfigurationService` → `MockConfigurationService`)
  - Concrete class replacement using `IServiceProvider` (`DbService` configuration override)
- **Execution Sequence Monitoring**: Detailed logging of WebApplicationFactory lifecycle methods (`ConfigureWebHost`, `ConfigureTestServices`, `CreateHost`) to understand the order of operations
- **Integration Test Examples**: Complete test scenarios demonstrating service replacement validation and execution sequence verification

The project includes a simple ASP.NET Core API with configuration-dependent services and comprehensive test coverage showing how to effectively mock and replace services during integration testing.
