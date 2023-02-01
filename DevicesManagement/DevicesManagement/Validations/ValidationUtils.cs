using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DevicesManagement.Validations;

public static class ValidationUtils
{
    public static class IPv4
    {
        public static readonly string REGEX_PATTERN = "^(0|[1-9][0-9]{0,2})(\\.(0|[1-9][0-9]{0,2})){3}:(0|[1-9][0-9]*)$";
        public static bool IsValid(string str)
        {
            var splitted = str.Split(':');
            if (splitted.Length > 2) return false;

            var ip = splitted.FirstOrDefault();
            if (ip is null) return false;

            var port = splitted.LastOrDefault();
            if (port is not null && !Int16.TryParse(port, out _)) return false;

            var numbers = ip.Split('.')
                .Where(s => Regex.IsMatch(s, "^([1-9]{1}[0-9]{0,2}|0)$"));
            if (numbers.Count() != 4) return false;

            return numbers.Select(value => Int16.Parse(value))
                .All(value => value <= 255);
        }
    }

    public static class Users
    {
        public static readonly string EMPLOYEE_ID_REGEX = "^[a-z]{4}[0-9]{8}$";
        public static readonly string PASSWORD_REGEX = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{8,32}$";
    }

    public static class Common
    {
        public static bool IsNotEmpty(object obj)
            => obj.GetType()
                .GetProperties()
                .Any(property => property.GetValue(obj) is not null);
    }
}
