using FluentValidation;

namespace Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidation()
    {
        RuleFor(c => c.employee.Name).NotEmpty();
        RuleFor(c => c.employee.Email).NotEmpty();
    }
}