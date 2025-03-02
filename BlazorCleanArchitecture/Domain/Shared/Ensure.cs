using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Shared;


public static class Ensure
{
    /// <summary>
    /// Ensures that the values is not null
    /// </summary>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}