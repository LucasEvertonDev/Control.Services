namespace ControlServices.WebApi.Structure.Pdf;

public interface IPdfService
{
    Task<byte[]> GeneratePdfAsync();
}