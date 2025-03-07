namespace Domain.Shared;


/// <summary>
/// Represents a standard validation error
/// </summary>
public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "General.Validation",
            "One or more validation errors occurred",
            ErrorType.Validation)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }
}
