using Domain.Abstractions;

namespace Domain.Features.Employee;

public class EmployeeErrors
{
    public static Error Exists(string name) =>
        Error.Failure("Employee.Exists", $"Employee with the name {name} allready exists");
    
    public static Error NotFound(int employeeId) =>
        Error.NotFound("Employee.NotFound", $"Employee with the name {employeeId} doesn't exist");
}