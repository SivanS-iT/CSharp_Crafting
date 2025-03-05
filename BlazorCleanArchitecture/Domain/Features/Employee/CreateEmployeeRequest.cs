namespace Domain.Features.Employee
{
    /// <summary>
    /// Class used to add employee to the database
    /// </summary>
    public sealed class CreateEmployeeRequest
    {
        public string Name { get; init; }
        public string? Address { get; init; }
        public string Email { get; init; }
    }
}
