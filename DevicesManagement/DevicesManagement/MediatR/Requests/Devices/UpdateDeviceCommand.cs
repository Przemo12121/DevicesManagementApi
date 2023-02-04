﻿using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Devices;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, UpdateDeviceCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<UpdateDeviceRequest, UpdateDeviceRequestValidator, UpdateDeviceCommand>),
    order: 2
)]
public class UpdateDeviceCommand : IRequest<IActionResult>, IRequestContainerCommand<UpdateDeviceRequest>, IResourceAuthorizableCommand<Device>
{
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
    public UpdateDeviceRequest Request { get; init; }
}
