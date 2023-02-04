namespace DevicesManagement.DataTransferObjects.Responses;

public sealed class CommandDto
{
    public Guid Id { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Body { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
};
