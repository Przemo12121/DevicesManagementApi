namespace DevicesManagement.DataTransferObjects.Requests;

public record EditCommandRequest
{
    public string? Name { get; init; }
    public string? Body { get; init; }
    public string? Description { get; init; }
};