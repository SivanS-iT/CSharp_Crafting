using FluentValidation;

namespace Application.Employees.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.Employee.Email).NotEmpty().WithErrorCode(EmployeeErrorCodes.EmailMissing);
        RuleFor(c => c.Employee.Name).NotEmpty().WithErrorCode(EmployeeErrorCodes.NameMissing);
    }
    
}