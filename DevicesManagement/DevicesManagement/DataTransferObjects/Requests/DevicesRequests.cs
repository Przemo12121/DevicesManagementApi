namespace DevicesManagement.DataTransferObjects.Requests;

public record RegisterCommandRequest
{
    public string Name { get; init; }
    public string Body { get; init; }
    public string? Description { get; init; }
};

public record RegisterDeviceRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
};