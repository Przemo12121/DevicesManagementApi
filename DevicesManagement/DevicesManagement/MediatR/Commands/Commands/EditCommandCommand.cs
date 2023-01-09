using Database.Models;
using MediatR;

namespace DevicesManagement.MediatR.Commands.Commands;

public class EditCommandCommand : IRequest<>, IResourceAuthorizableCommand<Command>
{
}
