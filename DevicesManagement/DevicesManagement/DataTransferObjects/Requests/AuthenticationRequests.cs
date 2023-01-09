namespace DevicesManagement.DataTransferObjects.Requests;

public record LoginWithCredentialsRequest
{
    public string Login { get; init; }
    public string Password { get; init; }
};