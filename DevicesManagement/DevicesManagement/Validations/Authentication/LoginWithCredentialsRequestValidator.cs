using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Authentication;

public class LoginWithCredentialsRequestValidator : AbstractValidator<LoginWithCredentialsRequest>
{
    public LoginWithCredentialsRequestValidator()
    {
        RuleFor(request => request.Login)
            .NotNull()
            .Matches(ValidationUtils.Users.EmployeeIdRegex);

        RuleFor(request => request.Password)
            .NotNull()
            .MinimumLength(1);
    }
}
