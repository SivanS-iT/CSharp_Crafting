namespace Domain.DTOs
{
    /// <summary>
    /// Response header 
    /// </summary>
    /// <param name="Flag"></param>
    /// <param name="Message"></param>
    public record ServiceResponse(bool Flag, string Message);
}
