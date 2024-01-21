namespace ControlServices.WebApi.Structure.Reports.PdfGenerator;

public interface IPdfService
{
    Task<byte[]> GeneratePdfAsync();
}