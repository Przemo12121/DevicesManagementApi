using DevicesManagement.DataTransferObjects;
using FluentValidation;

namespace DevicesManagement.Validations.Commands;

public class EditCommandRequestValidator : AbstractValidator<EditCommandRequest>
{
    public EditCommandRequestValidator()
    {
        RuleFor(request => request.Name).Length(1, 64);
        RuleFor(request => request.Body).Length(1, 2048);
        RuleFor(request => request.Description).Length(1, 4096);
    }
}
