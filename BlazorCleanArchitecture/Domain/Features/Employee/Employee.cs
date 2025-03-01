using Domain.Primitives;

namespace Domain.Features.Employee
{
    
    /// <summary>
    /// Employee entity
    /// </summary>
    public sealed class Employee : Entity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
    }
}
