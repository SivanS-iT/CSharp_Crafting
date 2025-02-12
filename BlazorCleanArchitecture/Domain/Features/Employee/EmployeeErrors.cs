using Domain.Abstractions;

namespace Domain.Features.Employee;

public class EmployeeErrors
{
    public static Error Exists(string name) =>
        Error.Failure("Employee.Exists", $"Empoyee with the name {name} allready exists");
}