using System.Diagnostics.CodeAnalysis;
using ControlServices.WebApi.Structure.Helpers;

namespace ControlServices.WebApi.Structure.Attributes;

public class HttpPutParamsAttribute<TParams> : HttpPutAttribute
    where TParams : class
{
    public HttpPutParamsAttribute()
        : base(Params.GetRoute(typeof(TParams)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpPutParamsAttribute{TParams}"/> class.
    /// </summary>
    /// <param name="template">The route template. May not be null.</param>
    public HttpPutParamsAttribute([StringSyntax("Route")] string template)
        : base(Params.GetRoute(typeof(TParams), template))
    {
    }
}