using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class EditEmployeeRequestValidator : AbstractValidator<EditEmployeeRequest>
{
    public EditEmployeeRequestValidator()
    {
        RuleFor(request => request)
            .Must(ValidationUtils.Common.IsNotEmpty);

        RuleFor(request => request.Name)
            .Length(1, 256);

        RuleFor(request => request.Password)
            .Matches(ValidationUtils.Users.PASSWORD_REGEX);

        RuleFor(request => request.EmployeeEid)
            .Matches(ValidationUtils.Users.EMPLOYEE_ID_REGEX);
    }
}
