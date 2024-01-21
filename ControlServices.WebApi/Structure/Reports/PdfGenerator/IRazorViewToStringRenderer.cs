namespace ControlServices.WebApi.Structure.Reports.PdfGenerator;

public interface IRazorViewToStringRenderer
{
    Task<string> RenderViewToStringAsync(string viewName);
}
