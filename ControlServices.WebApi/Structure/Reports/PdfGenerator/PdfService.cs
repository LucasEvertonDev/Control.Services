using PuppeteerSharp;

namespace ControlServices.WebApi.Structure.Reports.PdfGenerator;

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
        var htmlContent = await _razorViewToStringRenderer.RenderViewToStringAsync("/Structure/Reports/Index.cshtml");

        // Gera o PDF usando o IronPDF
        using var browserFetcher = new BrowserFetcher();

        await browserFetcher.DownloadAsync();

        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, });

        await using var page = await browser.NewPageAsync();

        await page.SetContentAsync(htmlContent);

        byte[] pdfBytes = await page.PdfDataAsync(new PdfOptions()
        {
            DisplayHeaderFooter = true,
            HeaderTemplate = "<header style='font-size: 12px; text-align: center;'>Cabeçalho</header>",
            FooterTemplate = "<footer style='font-size: 12px; text-align: center;'>Rodapé</footer>",
            PrintBackground = true,
            MarginOptions = new PuppeteerSharp.Media.MarginOptions()
            {
                Bottom = "80",
                Left = "20",
                Right = "20",
                Top = "150"
            }
        });

        await browser.CloseAsync();

        // Retorna o PDF como um array de bytes
        return pdfBytes;
    }
}