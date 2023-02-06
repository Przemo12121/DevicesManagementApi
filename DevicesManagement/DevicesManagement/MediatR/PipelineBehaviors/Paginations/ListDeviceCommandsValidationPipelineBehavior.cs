using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class ListDeviceCommandValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetEmployeesQuery>
{
    protected static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(32, new[] { "name", "body" }) };

    public ListDeviceCommandValidationPipelineBehavior() : base(_validators) { }
}
