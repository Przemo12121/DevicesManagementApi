namespace DevicesManagement.DataTransferObjects;

public record EditCommandRequest(string? Name, string? Body, string? Description);