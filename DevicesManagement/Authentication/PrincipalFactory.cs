using System.Security.Principal;

namespace Authentication;

internal static class PrincipalFactory
{
    public static IPrincipal CreateUserWithRole(string employeeId, string role)
    {
        var identity = new GenericIdentity(employeeId);
        return new GenericPrincipal(identity, new string[] { role });
    }
}
