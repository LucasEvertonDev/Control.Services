using System.Net;
using Authentication.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Authentication.WebApi.Structure.Filters;

public class ModelStateFilter : IAsyncActionFilter
{
    private readonly Type _apiControllerAttribute;

    public ModelStateFilter()
    {
        _apiControllerAttribute = typeof(ApiControllerAttribute);
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller.GetType().CustomAttributes.Any(x => x.AttributeType == _apiControllerAttribute))
        {
            if (!context.ModelState.IsValid)
            {
                Dictionary<object, object[]> dic = new ();

                context.ModelState.ToList().ForEach(x =>
                {
                    var erros = x.Value?.Errors.Select(e => e.ErrorMessage).ToArray();
                    if (erros.Any())
                    {
                        dic.Add(x.Key, erros);
                    }
                });

                context.Result = new BadRequestObjectResult(new ResponseError<Dictionary<object, object[]>>
                {
                    Type = "Invalid request",
                    HttpCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Errors = dic
                });
            }
            else
            {
                await next();
            }
        }
    }
}