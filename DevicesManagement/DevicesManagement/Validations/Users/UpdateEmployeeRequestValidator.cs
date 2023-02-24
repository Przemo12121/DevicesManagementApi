using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(request => request)
            .Must(ValidationUtils.Common.IsNotEmpty);

        RuleFor(request => request.Name)
            .Length(1, 256);

        RuleFor(request => request.Password)
            .Matches(ValidationUtils.Users.PasswordRegex);

        RuleFor(request => request.EmployeeId)
            .Matches(ValidationUtils.Users.EmployeeIdRegex);
    }
}
