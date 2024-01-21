using ControlServices.WebApi.Structure.Reports.PdfGenerator;

namespace ControlServices.WebApi.EndPoints;

public static class ReportsEndPoints
{
    public static IEndpointRouteBuilder AddReportsEndPoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var reportsEndpoints = app.MapGroup(route).WithTags(tag);

        reportsEndpoints.MapGet($"/",
            async ([FromServices] IMediator mediator, [FromServices] IPdfService pdfService, CancellationToken cancellationToken) =>
                {
                    var file = await pdfService.GeneratePdfAsync();
                    return Results.File(file, "application/octet-stream", "nome-do-arquivo.pdf");
                }).AllowAnonymous();
        return app;
    }
}
