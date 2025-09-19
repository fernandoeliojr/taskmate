namespace TaskMate.Application.Services
{
    public class PasswordService
    {
        // Faz o hash de uma senha
        public string HashPassword(string password)
        {
            // Usa o BCrypt para gerar o hash
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verifica se a senha bate com o hash
        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
