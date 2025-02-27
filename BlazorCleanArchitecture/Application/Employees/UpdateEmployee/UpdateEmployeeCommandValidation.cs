using FluentValidation;

namespace Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidation()
    {
        RuleFor(c => c.employee.Name).NotEmpty().WithErrorCode(EmployeeErrorCodes.EmailMissing);
        RuleFor(c => c.employee.Email).NotEmpty().WithErrorCode(EmployeeErrorCodes.NameMissing);
    }
}