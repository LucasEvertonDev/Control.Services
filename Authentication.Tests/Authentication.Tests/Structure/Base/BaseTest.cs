using System.Security.Claims;
using Authentication.Application.Domain.Plugins.JWT.Conts;
using Authentication.Tests.Structure.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Authentication.Tests.Structure.Base;

[TestCaseOrderer("Architecture.Tests.Structure.Filters.PriorityOrderer", "Architecture.Tests")]
public class BaseTest
{
    private readonly DependencyResolverHelper _serviceProvider;

    private readonly DependencyResolverHelper _serviceProviderAuth;

    public BaseTest()
    {
        var webHost = WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build();

        _serviceProvider = new DependencyResolverHelper(webHost);

        var webHost2 = WebHost.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServicesAuth)
            .UseStartup<Startup>()
            .Build();

        _serviceProviderAuth = new DependencyResolverHelper(webHost2);
    }

    protected DependencyResolverHelper ServiceProvider => _serviceProvider;

    protected DependencyResolverHelper ServiceProviderAuthenticated => _serviceProviderAuth;

    private void ConfigureServicesAuth(IServiceCollection services)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtUserClaims.Email, "teste@teste.com"),
            new Claim(JwtUserClaims.Id, "052E0B67-2AAF-44E1-82BD-08DBE2CEE631"),
        };

        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        IHttpContextAccessor context = new HttpContextAccessor()
        {
            HttpContext = new DefaultHttpContext()
            {
                User = user
            }
        };

        Thread.CurrentPrincipal = user;

        services.AddSingleton<IHttpContextAccessor>(context);
    }
}
