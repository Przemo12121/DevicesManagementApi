namespace DevicesManagement.DataTransferObjects.Requests;

public record UpdateCommandRequest
{
    public string? Name { get; init; }
    public string? Body { get; init; }
    public string? Description { get; init; }
};