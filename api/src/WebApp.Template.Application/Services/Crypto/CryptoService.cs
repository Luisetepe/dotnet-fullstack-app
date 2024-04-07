using System.Security.Cryptography;
using NSec.Cryptography;

namespace WebApp.Template.Application.Services.Crypto;

public class CryptoService : ICryptoService
{
    public string HashPassword(string password, byte[] salt)
    {
        var algorithm = PasswordBasedKeyDerivationAlgorithm.Argon2id(
            new Argon2Parameters
            {
                MemorySize = 1024,
                NumberOfPasses = 4,
                DegreeOfParallelism = 1
            }
        );
        var hash = algorithm.DeriveBytes(password, salt, 32);

        return Convert.ToBase64String(hash);
    }

    public bool VeryfyPassword(string password, string hash, byte[] salt)
    {
        var algorithm = PasswordBasedKeyDerivationAlgorithm.Argon2id(
            new Argon2Parameters
            {
                MemorySize = 1024,
                NumberOfPasses = 4,
                DegreeOfParallelism = 1
            }
        );
        var hashBytes = Convert.FromBase64String(hash);
        var newHash = algorithm.DeriveBytes(password, salt, 32);

        return newHash.SequenceEqual(hashBytes);
    }

    public byte[] GenerateSalt()
    {
        byte[] buffer = RandomNumberGenerator.GetBytes(16);

        return buffer;
    }
}
