using System.ComponentModel.DataAnnotations;

namespace Authentication.Tests.Structure.Services;
public static class ValidationService
{
    public static IList<ValidationResult> Validate<T>(T dominio)
        where T : class
    {
        var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
        var validationResults = new List<ValidationResult>();

        validator.TryValidateObjectRecursive(dominio, validationResults);

        return validationResults;
    }
}