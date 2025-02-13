using FluentValidation;

namespace Application.Employees.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.Employee.Email).NotEmpty();
        RuleFor(c => c.Employee.Name).NotEmpty();
    }
    
}