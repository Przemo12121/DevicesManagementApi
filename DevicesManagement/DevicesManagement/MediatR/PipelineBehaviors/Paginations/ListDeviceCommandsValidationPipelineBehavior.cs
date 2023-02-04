﻿using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.Validations.Common;

namespace DevicesManagement.MediatR.PipelineBehaviors.Paginations;

public class ListDeviceCommandValidationPipelineBehavior : RequestValidationPipelineBehavior<PaginationRequest, PaginationRequestValidator, GetEmployeesQuery>
{
    protected static List<PaginationRequestValidator> _validators = new(new[] { new PaginationRequestValidator(32, new[] { "name", "body" }) });

    public ListDeviceCommandValidationPipelineBehavior() : base(_validators) { }
}
