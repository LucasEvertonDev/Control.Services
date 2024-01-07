namespace ControlServices.Application.Domain.Plugins.Cryptography;

public interface IPasswordHash
{
    string EncryptPassword(string password, string passwordHash);

    string GeneratePasswordHash();

    bool PasswordIsEquals(string enteredPassword, string passwordHash, string storedPassword);
}