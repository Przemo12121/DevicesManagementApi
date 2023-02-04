using Authentication;
using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.ExtensionMethods;

public static class UserExtensions
{
    public static void UpdateWith(this User user, UpdateEmployeeRequest request, IIdentityProvider<User> identityProvider)
    {
        if (request.EmployeeId is not null)
            user.EmployeeId = request.EmployeeId;
        if (request.Name is not null) 
            user.Name = request.Name;
        if (request.Enabled is not null)
            user.Enabled = request.Enabled.Value;
        if (request.Password is not null)
            user.PasswordHashed = identityProvider.HashPassword(user, request.Password);

        user.UpdatedDate = DateTime.UtcNow;
    }
}
