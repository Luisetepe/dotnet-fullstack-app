namespace WebApp.Template.Application.Services.Crypto;

public interface ICryptoService
{
    string HashPassword(string password, byte[] salt);

    bool VeryfyPassword(string password, string hash, byte[] salt);

    byte[] GenerateSalt();
}
