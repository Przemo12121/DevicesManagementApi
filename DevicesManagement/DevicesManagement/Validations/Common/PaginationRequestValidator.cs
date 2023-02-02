using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Common;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator(int maxLimit, string[] orderKeys)
    {
        var alternativeOrderKesy = orderKeys.Aggregate((a, b) => a + '|' + b);

        RuleFor(request => request.Offset)
            .GreaterThan(-1);

        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(maxLimit);

        RuleFor(request => request.Order!.ToLower())
            .Matches($"({alternativeOrderKesy}):(asc|desc)")
            .When(request => request.Order is not null);
    }
}
