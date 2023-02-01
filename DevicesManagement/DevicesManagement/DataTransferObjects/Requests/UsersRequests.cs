namespace DevicesManagement.DataTransferObjects.Requests;

public record EditEmployeeRequest
{
    public string? Name { get; init; }
    public string? EmployeeEid { get; init; }
    public string? Password { get; init; }
};

public record RegisterEmployeeRequest
{
    public string Name { get; init; }
    public string EmployeeEid { get; init; }
    public string Password { get; init; }
};