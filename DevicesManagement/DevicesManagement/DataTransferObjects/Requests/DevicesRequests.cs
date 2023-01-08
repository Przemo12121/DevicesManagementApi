namespace DevicesManagement.DataTransferObjects.Requests;

public record CreateCommandRequest
{
    public string Name { get; init; }
    public string Body { get; init; }
    public string? Description { get; init; }
};

public record CreateDeviceRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
};