using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class ListUserDevicesValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, PaginationRequestValidator, GetEmployeesCommand, List<UserDto>>
{
    protected static List<PaginationRequestValidator> _validators = new(new[] { new PaginationRequestValidator(32, new[] { "name", "address" }) });

    public ListUserDevicesValidationPipelineBehavior() : base(_validators) { }
}
