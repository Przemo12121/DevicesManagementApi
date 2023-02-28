using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class GetEmployeesValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetEmployeesQuery>
{
    private static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(16, new[] { "name", "eid" }) };

    public GetEmployeesValidationPipelineBehavior() : base(_validators) { }
}
