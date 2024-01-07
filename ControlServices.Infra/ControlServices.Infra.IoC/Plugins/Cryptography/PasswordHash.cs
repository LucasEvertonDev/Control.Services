using System.Security.Cryptography;
using System.Text;
using ControlServices.Application.Domain.Plugins.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ControlServices.Infra.IoC.Plugins.Cryptography;

public class PasswordHash : IPasswordHash
{
    public string EncryptPassword(string password, string passwordHash)
    {
        string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(passwordHash),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        return encryptedPassw;
    }

    public bool PasswordIsEquals(string enteredPassword, string passwordHash, string storedPassword)
    {
        string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: enteredPassword,
            salt: Encoding.UTF8.GetBytes(passwordHash),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        return encryptedPassw == storedPassword;
    }

    public string GeneratePasswordHash()
    {
        byte[] salt = new byte[256 / 8]; // Generate a 128-bit salt using a secure PRNG
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }
}