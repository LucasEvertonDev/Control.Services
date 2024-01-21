using PuppeteerSharp;

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
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, });
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(htmlContent);
#pragma warning disable S1075 // URIs should not be hardcoded
        await page.PdfAsync(@"C:\\Temp\\teste3.pdf", new PdfOptions() { HeaderTemplate = "<P>Teste</p>", DisplayHeaderFooter = true, FooterTemplate = string.Empty });
#pragma warning restore S1075 // URIs should not be hardcoded

        // Retorna o PDF como um array de bytes
        return null;
    }

    public class TesteModel
    {
        public string Teste { get; set; }
    }
}