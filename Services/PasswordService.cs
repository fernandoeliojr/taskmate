using System.Security.Cryptography;

namespace TaskMate.Services;

public class PasswordService
{
    private const int Iterations = 100_000;
    private const int SaltSize = 16;
    private const int HashSize = 32;

    // Gera hash + salt (use ao registrar/seedar usuário)
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        if (password is null) throw new ArgumentNullException(nameof(password));
        using var rng = RandomNumberGenerator.Create();
        passwordSalt = new byte[SaltSize];
        rng.GetBytes(passwordSalt);

        passwordHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            passwordSalt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );
    }

    // Verifica a senha informada contra hash/salt armazenados
    public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        if (password is null) throw new ArgumentNullException(nameof(password));
        if (storedHash == null || storedSalt == null) return false;
        if (storedHash.Length != HashSize) return false;

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            storedSalt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        // comparação em tempo constante
        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }
}
