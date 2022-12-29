﻿namespace DevicesManagement.DataTransferObjects;

public record PaginationRequest 
{
    public int? Limit { get; init; }
    public int? Offset { get; init; }
    public string? Order { get; init; }
};