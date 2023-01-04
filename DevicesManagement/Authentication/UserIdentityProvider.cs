using Database.Models;
using Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Authentication;

public class UserIdentityProvider : IIdentityProvider<User>
{
    private readonly IUsersRepository<User> _usersRepository;
    private PasswordHasher<User> PasswordHasher { get; } = new PasswordHasher<User>();
    public UserIdentityProvider(IUsersRepository<User> usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public User? Identify(string keyName, string password)
    {
        var user = _usersRepository.FindByEmployeeId(keyName);
        if (user == null) return null;

        var result = PasswordHasher.VerifyHashedPassword(user, user.PasswordHashed, password);
        if (result == PasswordVerificationResult.Success) return user;

        return null;
    }

    public User CreateIdentity(string keyName, string name, string password, AccessLevel accessLevel)
    {
        var identity = new User
        {
            Id = new Guid(),
            CreatedDate = DateTime.UtcNow,
            AccessLevel = accessLevel,
            EmployeeId = keyName,
            UpdatedDate = DateTime.UtcNow,
            Name = name,
            Enabled = true
        };

        identity.PasswordHashed = PasswordHasher.HashPassword(identity, password);

        return identity;
    }

    public void SaveIdentity(User user) => _usersRepository.Add(user);
}
