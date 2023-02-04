namespace DevicesManagement;

public static class StringMessages
{
    public static class Successes
    {
        public static readonly string COMMAND_RUN = "Requested command run. Check devices messages for updates.";
    }

    public static class HttpErrors
    {
        public static readonly string INTERNAL = "Unexpected internal error occured.";
        public static readonly string ACTION_FAILED = "Action failed.";
        public static readonly string RESOURCE_NOT_FOUND = "Resource not found";
        public static readonly string UNAUTHORIZED_TO_RESOURCE = "Requested resource does not exist, or access is restricted.";
        public static readonly string EMPLOYEE_ID_TAKEN = "Reqested employee id is already taken.";
    }

    public static class InternalErrors
    {
        public static readonly string EMPLOYEE_ACCESS_LEVEL_NOT_FOUND = "Could not get employee access level.";
        public static readonly string SUBJECT_NOT_FOUND = "Could not find subject.";
        public static readonly string ROLE_NOT_FOUND = "Could not find role.";
        public static readonly string INVALID_ORDER_KEY = "Invalid ordery key.";
    }
}
