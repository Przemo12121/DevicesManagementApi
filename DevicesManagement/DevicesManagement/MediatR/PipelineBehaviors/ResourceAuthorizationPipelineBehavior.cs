using Database.Models.Base;
using Database.Models.Enums;
using Database.Repositories;
using Database.Repositories.Interfaces;
using DevicesManagement.Errors;
using DevicesManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class ResourceAuthorizationPipelineBehavior<TResource, TRequest> : IPipelineBehavior<TRequest, IActionResult>
    where TResource : IDatabaseModel
    where TRequest : IRequest<IActionResult>, IResourceAuthorizableCommand<TResource>
{
    protected readonly IHttpContextAccessor _httpContentAccessor;
    protected IResourceAuthorizableRepository<TResource> Repository { get; init; }

    public ResourceAuthorizationPipelineBehavior(IResourceAuthorizableRepository<TResource> repository, IHttpContextAccessor httpContentAccessor)
    {
        Repository = repository;
        _httpContentAccessor = httpContentAccessor;
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var isAuthorized = Authorize(request);
        if (!isAuthorized)
        {
            return ErrorResponses.CreateDetailed(
                StatusCodes.Status404NotFound,
                StringMessages.HttpErrors.Details.UNAUTHORIZED_TO_RESOURCE(
                    typeof(TResource).Name, 
                    request.ResourceId.ToString()
                )
            );
        }

        return await next();
    }

    protected bool Authorize(TRequest request)
    {
        var ownerId = _httpContentAccessor.HttpContext?.User.Identity?.Name 
            ?? throw new Exception(StringMessages.InternalErrors.SUBJECT_NOT_FOUND);

        var role = _httpContentAccessor.HttpContext?.User.Claims
            .Where(claim => claim.Type.Equals(ClaimTypes.Role))
            .SingleOrDefault() ?? throw new Exception(StringMessages.InternalErrors.ROLE_NOT_FOUND);

        // admin does not need to be owner of the resoruce
        bool isAdmin = role.Value.Equals(AccessLevels.Admin.ToString());
        var resource = isAdmin
            ? Repository.FindById(request.ResourceId)
            : Repository.FindByIdAndOwnerId(request.ResourceId, ownerId);

        request.Resource = resource!;

        return resource is not null;
    }
}
