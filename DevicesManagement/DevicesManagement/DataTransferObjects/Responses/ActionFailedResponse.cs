namespace DevicesManagement.DataTransferObjects.Responses;

public class ActionFailedResponse : Exception
{
    public string Status { get; } = StringMessages.HttpErrors.ACTION_FAILED;
    public string Reason { get; init; }
    public ActionFailedResponse(string reason)
    {
        Reason = reason;
    }
}