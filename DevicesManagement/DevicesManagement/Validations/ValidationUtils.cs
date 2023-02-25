using System.Net;
using System.Text.RegularExpressions;

namespace DevicesManagement.Validations;

public static class ValidationUtils
{
    public static class IPv4
    {
        public static bool IsValid(string str)
            => IPEndPoint.TryParse(str, out _);
    }

    public static class Users
    {
        public static readonly string EmployeeIdRegex = "^[a-z]{4}[0-9]{8}$";
        public static readonly string PasswordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{8,32}$";
    }

    public static class Common
    {
        public static bool IsNotEmpty(object obj)
            => obj.GetType()
                .GetProperties()
                .Any(property => property.GetValue(obj) is not null);
    }
}
