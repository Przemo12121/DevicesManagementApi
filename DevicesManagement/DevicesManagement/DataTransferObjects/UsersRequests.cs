namespace DevicesManagement.DataTransferObjects;

public record EditEmployeeRequest 
{
    public string? Name { get; init; }
    public string? EmployeeEid { get; init; }
    public string? Password { get; init; }
};

public record CreateEmployeeRequest
{
    public string Name { get; init; }
    public string EmployeeEid { get; init; }
    public string Password { get; init; }
};