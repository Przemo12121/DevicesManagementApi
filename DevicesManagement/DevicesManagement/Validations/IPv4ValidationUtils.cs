using System.Text.RegularExpressions;

namespace DevicesManagement.Validations;

public static class IPv4ValidationUtils
{
    public static readonly string REGEX_PATTERN = "^(0|[1-9][0-9]{0,2})(\\.(0|[1-9][0-9]{0,2})){3}:(0|[1-9][0-9]*)$";
    public static bool IsValid(string str)
    {
        var ip = str.Split(':')?.FirstOrDefault();
        if (ip is null) return false;

        var splitted = ip.Split('.').Where(s => Regex.IsMatch(s, "^[0-9]{1,3}$"));
        if (splitted.Count() != 4) return false;

        return splitted.Select(value => Int16.Parse(value))
            .All(value => value <= 255);
    }
}
