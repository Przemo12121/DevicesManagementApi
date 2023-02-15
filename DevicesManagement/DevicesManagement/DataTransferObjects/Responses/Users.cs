namespace DevicesManagement.DataTransferObjects.Responses;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string EmployeeId { get; set; }
    public DateTime UpdatedDate { get; set; }

    public bool Enabled { get; set; }

    // TODO access level?
}
