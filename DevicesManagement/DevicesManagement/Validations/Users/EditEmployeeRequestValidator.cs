using DevicesManagement.DataTransferObjects;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class EditEmployeeRequestValidator : AbstractValidator<EditEmployeeRequest>
{
    public EditEmployeeRequestValidator()
    {
        RuleFor(request => request.Name).Length(1, 256);
        RuleFor(request => request.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{8,32}$");
        RuleFor(request => request.EmployeeEid).Matches("^[a-z]{4}[0-9]{8}$");
    }
}
