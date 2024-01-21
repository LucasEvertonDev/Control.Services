namespace ControlServices.WebApi.Structure.Pdf;

public class PdfService : IPdfService
{
    private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

    public PdfService(IRazorViewToStringRenderer razorViewToStringRenderer)
    {
        _razorViewToStringRenderer = razorViewToStringRenderer;
    }

    public async Task<byte[]> GeneratePdfAsync()
    {
        // Renderiza a Razor Page para HTML
        var htmlContent = await _razorViewToStringRenderer.RenderViewToStringAsync<IndexModel>("/Structure/Pdf/Index.cshtml", new IndexModel());

        // Gera o PDF usando o IronPDF
        var ironPdf = new ChromePdfRenderer();
        var pdf = ironPdf.RenderHtmlAsPdf(htmlContent);

        // Retorna o PDF como um array de bytes
        return pdf.BinaryData;
    }

    public class TesteModel
    {
        public string Teste { get; set; }
    }
}