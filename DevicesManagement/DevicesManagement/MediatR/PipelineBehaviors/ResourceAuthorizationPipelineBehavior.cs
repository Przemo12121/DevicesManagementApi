using Database.Models.Base;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.Exceptions;
using DevicesManagement.MediatR.Commands;
using MediatR;
using System.Security.Claims;

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

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Authorize(request);
        
        return await next();
    }

    protected void Authorize(TRequest request)
    {
        var ownerId = _httpContentAccessor.HttpContext?.User.Identity?.Name ?? throw new Exception("Employee Id not present.");
        var role = _httpContentAccessor.HttpContext?.User.Claims
            .Where(claim => claim.Type.Equals(ClaimTypes.Role))
            .SingleOrDefault() ?? throw new Exception("Role not present.");

        // admin does not need to be owner of the resoruce
        bool isAdmin = role.Value.Equals(AccessLevels.Admin.ToString());
        var resource = isAdmin
            ? Repository.FindById(request.ResourceId)
            : Repository.FindByIdAndOwnerId(request.ResourceId, ownerId);

        if (resource is null)
            throw isAdmin ? new NotFoundHttpException() : new ForbiddenHttpException();

        request.Resource = resource;
    }
}
