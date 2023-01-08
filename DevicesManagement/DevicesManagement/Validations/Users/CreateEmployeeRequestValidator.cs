using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().Length(1, 256);
        RuleFor(request => request.Password).NotNull().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{8,32}$");
        RuleFor(request => request.EmployeeEid).NotNull().Matches("^[a-z]{4}[0-9]{8}$");
    }
}
