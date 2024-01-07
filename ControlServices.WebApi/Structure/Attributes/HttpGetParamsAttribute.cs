using System.Diagnostics.CodeAnalysis;

namespace ControlServices.WebApi.Structure.Attributes;

public class HttpGetParamsAttribute<TParams> : HttpGetAttribute
    where TParams : class
{
    public HttpGetParamsAttribute()
        : base(Params.GetRoute(typeof(TParams)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpGetParamsAttribute{TParams}"/> class.
    /// </summary>
    /// <param name="template">The route template. May not be null.</param>
    public HttpGetParamsAttribute([StringSyntax("Route")] string template)
        : base(Params.GetRoute(typeof(TParams), template))
    {
    }
}
