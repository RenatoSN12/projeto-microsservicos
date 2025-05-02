using FluentValidation.Results;

namespace Emitix.ProductService.Common.Extensions;

public static class ValidationExtensions
{
    public static string ToMessageString(this List<ValidationFailure> failures)
    {
        return string.Join("; ", failures.Select(f => f.ErrorMessage));
    }
}