using EntraIDCustomAuthenticationExtensionsInProcess;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]


namespace EntraIDCustomAuthenticationExtensionsInProcess;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
    }
}