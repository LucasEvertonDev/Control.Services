using System.Text;
using ControlServices.Application.Domain;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Infra.IoC;
using ControlServices.WebApi.EndPoints;
using ControlServices.WebApi.Structure.Filters;
using ControlServices.WebApi.Structure.Middlewares;
using ControlServices.WebApi.Structure.PolicyProviders;
using ControlServices.WebApi.Structure.Reports.PdfGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Swashbuckle.AspNetCore.Filters;

namespace ControlServices.WebApi.Structure.Startup;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = new AppSettings();

        services.AddSingleton<ExceptionFilter>();

        configuration.Bind(appSettings);

        services.AddMvc(x =>
        {
            if (appSettings.FilterExceptions)
            {
                x.Filters.Add(typeof(ExceptionFilter));
            }

            x.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseError<Dictionary<object, object[]>>), 400));
        }).ConfigureApiBehaviorOptions(x => { x.SuppressModelStateInvalidFilter = true; });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        // Binding model
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

        services.AddScoped<IPdfService, PdfService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddSwaggerGen(c =>
        {
            c.RegisterSwaggerDefaultConfig(true, appSettings.Swagger.FlowLogin);
        });

        // services.AddSingleton<IAuthorizationHandler, PermissionHandler>()
        services.AddSingleton<AppSettings>(appSettings);

        services.AddSingleton<IAuthorizationPolicyProvider, DatabasePolicyProvider>();

        services.AddSwaggerExamples();

        services.AddInfraStructure(appSettings);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<AuthUnauthorizedMiddleware>();

        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMetricServer();

        app.UseHttpMetrics();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .MapGroup("api/v1/")
                .WithOpenApi()
                .AddAuthEndpoints("auth", "Auth")
                .AddUsuariosEndpoint("usuarios", "Usuarios")
                .AddClientesEndpoint("clientes", "Clientes")
                .AddServicosEndpoint("servicos", "Servicos")
                .AddCustosEndpoint("custos", "Custos")
                .AddAtendimentosEndpoint("atendimentos", "Atendimentos")
                .AddReportsEndPoints("reports", "Reports")
                ;
            endpoints.MapMetrics().RequireAuthorization("ReadMetrics");
        });
    }
}
