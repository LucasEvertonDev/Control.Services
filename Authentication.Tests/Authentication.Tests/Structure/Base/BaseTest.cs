using Authentication.Tests.Structure.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Authentication.Tests.Structure.Base;

[TestCaseOrderer("Architecture.Tests.Structure.Filters.PriorityOrderer", "Architecture.Tests")]
public class BaseTest
{
    private readonly DependencyResolverHelper _serviceProvider;

    public BaseTest()
    {
        var webHost = WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();

        _serviceProvider = new DependencyResolverHelper(webHost);
    }

    protected DependencyResolverHelper ServiceProvider => _serviceProvider;
}
