using System.Runtime.InteropServices.JavaScript;
using Domain.Shared;

namespace Domain.Features.Employee;

public class EmployeeErrors
{
    public static Error NotFound(int employeeId) =>
        Error.NotFound("Employee.NotFound", $"Employee with the name {employeeId} doesn't exist");

    public static readonly Error EmployeeExists = 
            Error.Problem("Employee.Exists", $"Employee with the given Email already exists");
 }