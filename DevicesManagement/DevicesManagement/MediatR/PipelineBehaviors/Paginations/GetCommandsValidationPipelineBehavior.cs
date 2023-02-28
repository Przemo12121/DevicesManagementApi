using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class GetCommandsValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetCommandsQuery>
{
    private static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(96, new[] { "name", "body" }) };

    public GetCommandsValidationPipelineBehavior() : base(_validators) { }
}
