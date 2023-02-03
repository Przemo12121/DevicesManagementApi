﻿using Database.Models.Base;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.Exceptions;
using DevicesManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class ResourceAuthorizationPipelineBehavior<TResource, TResourceRepository, TRequest> : IPipelineBehavior<TRequest, IActionResult>
    where TResource : IDatabaseModel
    where TResourceRepository : IResourceAuthorizableRepository<TResource>
    where TRequest : IRequest<IActionResult>, IResourceAuthorizableCommand<TResource>
{
    protected readonly IHttpContextAccessor _httpContentAccessor;
    protected TResourceRepository Repository { get; init; }

    public ResourceAuthorizationPipelineBehavior(TResourceRepository repository, IHttpContextAccessor httpContentAccessor)
    {
        Repository = repository;
        _httpContentAccessor = httpContentAccessor;
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var (isAuthorized, error) = Authorize(request);
        if (!isAuthorized)
            return error!;

        return await next();
    }

    protected (bool, IActionResult?) Authorize(TRequest request)
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

        var error = resource is null
            ? new NotFoundObjectResult(StringMessages.HttpErrors.RESOURCE_NOT_FOUND)
            : null;

        request.Resource = resource!;

        return (resource is not null, error);
    }
}
