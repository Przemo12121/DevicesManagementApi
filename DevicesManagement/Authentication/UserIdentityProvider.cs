using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Authentication;

public class UserIdentityProvider : IIdentityProvider<User>
{
    private readonly IUsersRepository _usersRepository;
    private PasswordHasher<User> PasswordHasher { get; } = new();
    public UserIdentityProvider(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<User?> Identify(string keyName, string password)
    {
        var user = await _usersRepository.FindByEmployeeIdAsync(keyName);
        if (user is null) return null;

        var result = PasswordHasher.VerifyHashedPassword(user, user.PasswordHashed, password);
        if (result.Equals(PasswordVerificationResult.Success)) return user;

        return null;
    }

    public User CreateIdentity(string keyName, string name, string password, AccessLevel accessLevel)
    {
        var identity = new User
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            AccessLevel = accessLevel,
            EmployeeId = keyName,
            UpdatedDate = DateTime.UtcNow,
            Name = name,
            Enabled = true
        };

        identity.PasswordHashed = HashPassword(identity, password);

        return identity;
    }


    public string HashPassword(User identity, string password)
        => PasswordHasher.HashPassword(identity, password);
}
