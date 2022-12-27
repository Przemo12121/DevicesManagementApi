namespace DevicesManagement.DataTransferObjects;

public record PaginationRequest(int? Limit, int? Offset, string? Order);