using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class RegisterEmployeeRequestValidator : AbstractValidator<RegisterEmployeeRequest>
{
    public RegisterEmployeeRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .Length(1, 256);

        RuleFor(request => request.Password)
            .NotNull()
            .Matches(ValidationUtils.Users.PASSWORD_REGEX);

        RuleFor(request => request.EmployeeId)
            .NotNull()
            .Matches(ValidationUtils.Users.EMPLOYEE_ID_REGEX);
    }
}
