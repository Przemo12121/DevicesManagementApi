using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class GetCommandsValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetCommandsQuery>
{
    protected static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(32, new[] { "name", "body" }) };

    public GetCommandsValidationPipelineBehavior() : base(_validators) { }
}
