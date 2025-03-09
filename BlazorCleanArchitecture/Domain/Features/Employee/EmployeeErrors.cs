using Domain.Shared;

namespace Domain.Features.Employee;

/// <summary>
/// Represents common errors for Employee entity
/// </summary>
public static class EmployeeErrors
{
    public static Error NotFound(int employeeId) =>
        Error.NotFound("Employee.NotFound", $"Employee with the id {employeeId} doesn't exist");

    public static readonly Error EmployeeExists = 
            Error.Problem("Employee.Exists", $"Employee with the given Email already exists");
 }