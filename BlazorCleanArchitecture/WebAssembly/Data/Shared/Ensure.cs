using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WebAssembly.Data.Shared;


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

    
    
    /// <summary>
    /// Ensures that the value is not null or empty
    /// </summary>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}