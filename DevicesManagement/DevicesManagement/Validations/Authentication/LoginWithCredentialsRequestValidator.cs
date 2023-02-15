using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Authentication;

public class LoginWithCredentialsRequestValidator : AbstractValidator<LoginWithCredentialsRequest>
{
    public LoginWithCredentialsRequestValidator()
    {
        RuleFor(request => request.Login)
            .NotNull()
            .Matches(ValidationUtils.Users.EMPLOYEE_ID_REGEX);

        RuleFor(request => request.Password)
            .NotNull()
            .MinimumLength(1);
    }
}
