using ControlServices.WebApi.Structure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ControlServices.WebApi.Structure.Extensions;

public static class SwaggerExtensions
{
    public static void RegisterSwaggerDefaultConfig(this SwaggerGenOptions options, bool useJwt, string loginUrl)
    {
        options.ExampleFilters();

        if (useJwt)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(loginUrl),
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                            { "TokenJWT", new OpenApiBoolean(true) },
                        },
                    }
                }
            });

            options.OperationFilter<SecureEndpointAuthRequirementFilter>();
        }

        options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

        options.TagActionsBy(api =>
        {
            if (api.GroupName != null)
            {
                return new[] { api.GroupName };
            }

            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return new[] { controllerActionDescriptor.ControllerName };
            }

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });

        options.DocInclusionPredicate((name, api) => true);

        options.OperationFilter<SwaggerDefaultValues>();
    }
}

internal class SecureEndpointAuthRequirementFilter : IOperationFilter
{
    public static OpenApiSecurityScheme OAuthScheme => new ()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription
            .ActionDescriptor
            .EndpointMetadata
            .OfType<AuthorizeAttribute>()
            .Any())
        {
            return;
        }

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement()
            {
                { OAuthScheme, new List<string>() }
            }
        };
    }
}