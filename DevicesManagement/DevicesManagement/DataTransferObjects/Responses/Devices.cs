namespace DevicesManagement.DataTransferObjects.Responses;

public sealed class DeviceDto
{
    public Guid Id { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Address { get; set; }
    public string Name { get; set; }

    public string EmployeeId { get; set; }
}
