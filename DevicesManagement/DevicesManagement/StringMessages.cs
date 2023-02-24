namespace DevicesManagement;

public static class StringMessages
{
    public static class Successes
    {
        public static readonly string COMMAND_RUN = "Requested command run. Check devices messages for updates.";
    }

    public static class HttpErrors
    {
        public static class Titles
        {           
            public static readonly string Internal = "Unexpected internal error occured";
            public static readonly string ResourceNotFound = "Resource not found";
            public static readonly string Unauthorized = "Unauthorized";
            public static readonly string Forbidden = "Access is forbidden";
            public static readonly string Conflict = "Conflciting state of the server";
            public static readonly string BadRequest = "Request is invalid";
        }

        public static class Details
        {
            public static readonly string Internal = "Unexpected error occurred.";
            public static readonly string EmployeeIdTaken = "Reqested employee id is already taken.";
            public static readonly string Unauthorized = "Unauthorized";
            public static readonly string InvalidCredentials = "Invalid credentials.";
            public static string UnauthorizedToResource(string type, string id) => $"Requested {type} resource with id {id} does not exist, or access is restricted.";
            
        }
    }

    public static class InternalErrors
    {
        public static readonly string EMPLOYEE_ACCESS_LEVEL_NOT_FOUND = "Could not get employee access level.";
        public static readonly string SUBJECT_NOT_FOUND = "Could not find subject.";
        public static readonly string ROLE_NOT_FOUND = "Could not find role.";
        public static readonly string INVALID_ORDER_KEY = "Invalid ordery key.";
        public static readonly string BAD_TYPE = "Bad object type passed to method.";
    }
}
