using Database.Models.Base;
using Database.Repositories.Interfaces;
using DevicesManagement.Exceptions;
using DevicesManagement.MediatR.Commands;
using MediatR;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class ResourceAuthorizationPipelineBehavior<TResource, TResourceRepository, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResource : IDatabaseModel
    where TResourceRepository : IResourceAuthorizableRepository<TResource>
    where TRequest : IRequest<TResponse>, IResourceAuthorizableCommand<TResource>
{
    protected readonly IHttpContextAccessor _httpContentAccessor;
    protected TResourceRepository Repository { get; init; }

    public ResourceAuthorizationPipelineBehavior(TResourceRepository repository, IHttpContextAccessor httpContentAccessor)
    {
        Repository = repository;
        _httpContentAccessor = httpContentAccessor;
    }

    protected ResourceAuthorizationResult Authorize(Guid resourceId, string employeeId)
    {
        var resource = Repository.FindByIdAndEmployeeId(resourceId, employeeId);

        return new ResourceAuthorizationResult(resource != null, resource);
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // TODO admin overtakes ?
        var ownerId = _httpContentAccessor.HttpContext?.User.Identity?.Name ?? throw new Exception("Employee Id not present.");
        var result = Authorize(request.ResourceId, ownerId);

        if (!result.IsAuhorized)
        {
            throw new ForbiddenException();
        }

        return await next();
    }

    protected record ResourceAuthorizationResult(bool IsAuhorized, TResource? Resource);
}

