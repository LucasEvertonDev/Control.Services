namespace Authentication.Application.Domain.Structure.Models;

#pragma warning disable S2326 // Unused type parameters should be removed
public interface IValidationAsync<TValidator> : IValidationAsync
    where TValidator : class
#pragma warning restore S2326 // Unused type parameters should be removed
{
}

public interface IValidationAsync
{
}