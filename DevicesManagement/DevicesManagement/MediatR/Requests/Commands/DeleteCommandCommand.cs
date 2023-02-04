﻿using Database.Models;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, DeleteCommandCommand>),
    order: 1
)]
public class DeleteCommandCommand 
    : IRequest<IActionResult>, IResourceAuthorizableCommand<Command>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
}