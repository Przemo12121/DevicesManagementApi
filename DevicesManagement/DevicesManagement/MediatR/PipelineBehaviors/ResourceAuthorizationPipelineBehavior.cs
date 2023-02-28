using Database.Models.Base;
using Database.Models.Enums;
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
    protected IResourceAuthorizableRepository<TResource> Repository { get; }

    public ResourceAuthorizationPipelineBehavior(IResourceAuthorizableRepository<TResource> repository, IHttpContextAccessor httpContentAccessor)
    {
        Repository = repository;
        _httpContentAccessor = httpContentAccessor;
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var resource = await Authorize(request);
        if (resource is null)
        {
            return ErrorResponses.CreateDetailed(
                StatusCodes.Status404NotFound,
                StringMessages.HttpErrors.Details.UnauthorizedToResource(
                    typeof(TResource).Name, 
                    request.ResourceId.ToString()
                )
            );
        }
        request.Resource = resource;

        return await next();
    }

    private async Task<TResource?> Authorize(TRequest request)
    {
        var ownerId = _httpContentAccessor.HttpContext?.User.Identity?.Name 
            ?? throw new Exception(StringMessages.InternalErrors.SUBJECT_NOT_FOUND);

        var role = _httpContentAccessor.HttpContext?.User.Claims
            .SingleOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)) 
            ?? throw new Exception(StringMessages.InternalErrors.ROLE_NOT_FOUND);

        // admin does not need to be owner of the resoruce
        bool isAdmin = role.Value.Equals(AccessLevels.Admin.ToString());
        var resource = isAdmin
            ? await Repository.FindByIdAsync(request.ResourceId)
            : await Repository.FindByIdAndOwnerIdAsync(request.ResourceId, ownerId);

        return resource;
    }
}
