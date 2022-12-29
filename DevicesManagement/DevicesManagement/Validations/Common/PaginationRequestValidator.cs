using DevicesManagement.DataTransferObjects;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace DevicesManagement.Validations.Common;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator(int maxLimit, string orderKey)
    {
        RuleFor(request => request.Offset).GreaterThan(-1);
        RuleFor(request => request.Limit).GreaterThan(0).LessThanOrEqualTo(maxLimit);
        RuleFor(request => request.Order.IsNullOrEmpty() 
            ? request.Order 
            : request.Order!.ToLower()
            ).Matches($"{orderKey}:(asc|desc)");
    }
}
