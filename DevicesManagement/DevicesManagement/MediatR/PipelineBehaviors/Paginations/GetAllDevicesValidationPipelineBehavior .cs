using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class GetAllDevicesValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetAllDevicesQuery>
{
    protected static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(32, new[] { "name", "address", "eid" }) };

    public GetAllDevicesValidationPipelineBehavior() : base(_validators) { }
}
