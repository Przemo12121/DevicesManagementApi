namespace DevicesManagement.DataTransferObjects;

public record EditEmployeeRequest(string? Name, string? EmployeeEid, string? Password);
public record CreateEmployeeRequest(string Name, string EmployeeEid, string Password);