namespace DevicesManagement.Validations;

public static class UsersValidationUtils
{
    public static readonly string EMPLOYEE_ID_REGEX = "^[a-z]{4}[0-9]{8}$";
    public static readonly string PASSWORD_REGEX = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{8,32}$";
}
