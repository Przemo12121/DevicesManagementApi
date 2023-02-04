namespace DevicesManagement.DataTransferObjects.Requests;

public interface IEmployeeIdContainer
{
    public string? EmployeeId { get; }
}

public record UpdateEmployeeRequest : IEmployeeIdContainer
{
    public string? Name { get; init; }
    public string? EmployeeId { get; init; }
    public string? Password { get; init; }
    public bool? Enabled { get; init; }
};

public record RegisterEmployeeRequest : IEmployeeIdContainer
{
    public string Name { get; init; }
    public string EmployeeId { get; init; }
    public string Password { get; init; }
};