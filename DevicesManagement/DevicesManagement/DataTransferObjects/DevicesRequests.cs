namespace DevicesManagement.DataTransferObjects;

public record CreateCommandRequest(string Name, string Body, string? Description);
public record CreateDeviceRequest(string Name, string Address);