namespace DevicesManagement.DataTransferObjects.Responses;

public class BadRequestResponse : Exception
{
    public IEnumerable<KeyValuePair<string, IEnumerable<string>>> FieldsWithErrors { get; init; }
    public BadRequestResponse(IEnumerable<KeyValuePair<string, IEnumerable<string>>> fieldsWithErrors) 
    {
        FieldsWithErrors = fieldsWithErrors;
    }
}
