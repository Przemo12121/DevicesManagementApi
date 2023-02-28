using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;
using FluentValidation;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class GetUserDevicesValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, GetUserDevicesQuery>
{
    private static List<IValidator<PaginationRequest>> _validators = new() { new PaginationRequestValidator(32, new[] { "name", "address" }) };

    public GetUserDevicesValidationPipelineBehavior() : base(_validators) { }
}
