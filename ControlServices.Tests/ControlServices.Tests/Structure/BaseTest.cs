using System.Linq.Expressions;
using ControlServices.Tests.Structure.Factorys;
using ControlServices.WebApi;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Notification.Notifications.Helpers;
using Refit;

namespace ControlServices.Tests.Structure;

[TestCaseOrderer("ControlServices.Tests.Structure.Filters.PriorityOrderer", "ControlServices.Tests")]
public class BaseTestInMemoryDb : IClassFixture<IntegrationTestInMemoryDbFactory<Program>>
{
    public BaseTestInMemoryDb(IntegrationTestInMemoryDbFactory<Program> integrationTestFactory)
    {
        HttpClient = integrationTestFactory.CreateClient();

        ServiceProvider = integrationTestFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
    }

    protected HttpClient HttpClient { get; }

    protected IServiceProvider ServiceProvider { get; }

    protected static RefitSettings GetSettings()
    {
        return new RefitSettings()
        {
            ExceptionFactory = (response) =>
            {
                return Task.FromResult((Exception)null);
            }
        };
    }

    protected TApi InstanceApi<TApi>()
    {
        return RestService.For<TApi>(HttpClient, GetSettings());
    }

    protected static string MemberName<TClass>(Expression<Func<TClass, dynamic>> exp)
    {
        return string.Join(string.Empty, ResultServiceHelpers.TranslateLambda(exp).Split(".").Skip(1).ToList());
    }

    protected static (string nome, string email, string senha) CriaUsuario()
    {
        return (new Faker().Person.FullName, new Faker().Person.Email, new Faker().Internet.Password(10));
    }
}

[TestCaseOrderer("ControlServices.Tests.Structure.Filters.PriorityOrderer", "ControlServices.Tests")]
public class BaseTestInDatabase : IClassFixture<IntegrationTestInDatabaseFactory<Program>>
{
    public BaseTestInDatabase(IntegrationTestInDatabaseFactory<Program> integrationTestFactory)
    {
        HttpClient = integrationTestFactory.CreateClient();

        ServiceProvider = integrationTestFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
    }

    protected HttpClient HttpClient { get; }

    protected IServiceProvider ServiceProvider { get; }

    protected static RefitSettings GetSettings()
    {
        return new RefitSettings()
        {
            ExceptionFactory = (response) =>
            {
                return Task.FromResult((Exception)null);
            }
        };
    }
    protected TApi InstanceApi<TApi>()
    {
        return RestService.For<TApi>(HttpClient, GetSettings());
    }

    protected static string MemberName<TClass>(Expression<Func<TClass, dynamic>> exp)
    {
        return string.Join(string.Empty, ResultServiceHelpers.TranslateLambda(exp).Split(".").Skip(1).ToList());
    }

    protected static (string nome, string email, string senha) CriaUsuario()
    {
        return (new Faker().Person.FullName, new Faker().Person.Email, new Faker().Internet.Password(10));
    }
}
